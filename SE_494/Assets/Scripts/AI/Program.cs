using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using UnityEngine;
using Random = UnityEngine.Random;


// This code was written with q-learning, a Reinforcement Learning method, in order to teach Non player characters in the 
// Merchant game to make the right decisions in the Game. These right decisions are about changing cities and trading within the game.
// The aim is for the Non Player Character to make the most profitable trade and go to the most profitable city. Multiple parameters 
// are calculated while making all the decisions.The main ones are: Supply/Demand ratio, distance of cities, price of items etc. 
// q-learning works on the principles of Markov Decision process. Depending on the reward system, a calculation is made according to the 
// decisions made by the NPC, and in the next stages, the NPC finds the most accurate paths from the highest q values.

public static class Program 
{
    // Define the q-table
    static double[,] q_table = new double[15, 7];
    
    //Action list of the AI merchant
    static Dictionary<string, int> acts = new Dictionary<string, int>(){
        {"goMine", 0}, {"goShopA", 1}, {"goShopB", 2}, {"goShopC", 3},
        {"goMilitary", 4}, {"Buy", 5}, {"Sell", 6}
    };
    
    //State list of AI merchant
    static Dictionary<string, int> states = new Dictionary<string, int>(){
        {"MineEmpty", 0}, {"MineHalfFull", 1}, {"MineFull", 2},
        {"AEmpty", 3}, {"AHalfFull", 4}, {"AFull", 5},
        {"BEmpty", 6}, {"BHalfFull", 7}, {"BFull", 8},
        {"CEmpty", 9}, {"CHalfFull", 10}, {"CFull", 11},
        {"MilitaryEmpty", 12}, {"MilitaryHalfFull", 13}, {"MilitaryFull", 14}
    };
    
    //Location table of cities
    static Dictionary<string, int> location_table = new Dictionary<string, int>(){
        {"Mine", 1}, {"A", 3}, {"B", 6}, {"C", 9}, {"Military", 12}
    };
    
    //Inventory of AI merchant
    static Dictionary<string, Dictionary<string, int>> inventory = new Dictionary<string, Dictionary<string, int>>
 {
     {
         "Sword", new Dictionary<string, int>
         {
             {"Exist", 0},
             {"cost", 14}
         }
     },
     {
         "Axe", new Dictionary<string, int>
         {
             {"Exist", 0},
             {"cost", 14}
         }
     },
     {
         "Shield", new Dictionary<string, int>
         {
             {"Exist", 0},
             {"cost", 14}
         }
     }
 };
    
//Supply list of every shops for every items and their costs
static Dictionary<string, Dictionary<string, Dictionary<string, int>>> supply_table = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>
{
    {
        "Mine", new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Sword", new Dictionary<string, int>
                {
                    {"supply", 5},
                    {"cost", 15}
                }
            },
            {
                "Axe", new Dictionary<string, int>
                {
                    {"supply", 3},
                    {"cost", 19}
                }
            },
            {
                "Shield", new Dictionary<string, int>
                {
                    {"supply", 4},
                    {"cost", 17}
                }
            }
        }
    },
    {
        "A", new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Sword", new Dictionary<string, int>
                {
                    {"supply", 5},
                    {"cost", 15}
                }
            },
            {
                "Axe", new Dictionary<string, int>
                {
                    {"supply", 7},
                    {"cost", 10}
                }
            },
            {
                "Shield", new Dictionary<string, int>
                {
                    {"supply", 9},
                    {"cost", 10}
                }
            }
        }
    },
    {
        "B", new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Sword", new Dictionary<string, int>
                {
                    {"supply", 5},
                    {"cost", 16}
                }
            },
            {
                "Axe", new Dictionary<string, int>
                {
                    {"supply", 4},
                    {"cost", 18}
                }
            },
            {
                "Shield", new Dictionary<string, int>
                {
                    {"supply", 2},
                    {"cost", 20}
                }
            }
        }
    },
    {
        "C", new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Sword", new Dictionary<string, int>
                {
                    {"supply", 6},
                    {"cost", 20}
                }
            },
            {
                "Axe", new Dictionary<string, int>
                {
                    {"supply", 7},
                    {"cost", 14}
                }
            },
            {
                "Shield", new Dictionary<string, int>
                {
                    {"supply", 6},
                    {"cost", 18}
                }
            }
        }
    },
    {
        "Military", new Dictionary<string, Dictionary<string, int>>
        {
            {
                "Sword", new Dictionary<string, int>
                {
                    {"supply", 2},
                    {"cost", 12}
                }
            },
            {
                "Axe", new Dictionary<string, int>
                {
                    {"supply", 7},
                    {"cost", 18}
                }
            },
            {
                "Shield", new Dictionary<string, int>
                {
                    {"supply", 6},
                    {"cost", 16}
                }
            }
        }
    }
};

//Demand list of every shops for every items and their costs
static Dictionary<string, Dictionary<string, Dictionary<string, int>>> demand_table = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>(){
    {"Mine", new Dictionary<string, Dictionary<string, int>>(){
        {"Sword", new Dictionary<string, int>(){ {"demand", 2}, {"cost", 12} }},
        {"Axe", new Dictionary<string, int>(){ {"demand", 3}, {"cost", 21} }},
        {"Shield", new Dictionary<string, int>(){ {"demand", 9}, {"cost", 24} }}
    }},
    {"A", new Dictionary<string, Dictionary<string, int>>(){
        {"Sword", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 18} }},
        {"Axe", new Dictionary<string, int>(){ {"demand", 3}, {"cost", 17} }},
        {"Shield", new Dictionary<string, int>(){ {"demand", 2}, {"cost", 16} }}
    }},
    {"B", new Dictionary<string, Dictionary<string, int>>(){
        {"Sword", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 18} }},
        {"Axe", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 20} }},
        {"Shield", new Dictionary<string, int>(){ {"demand", 9}, {"cost", 28} }}
    }},
    {"C", new Dictionary<string, Dictionary<string, int>>(){
        {"Sword", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 22} }},
        {"Axe", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 27} }},
        {"Shield", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 19} }}
    }},
    {"Military", new Dictionary<string, Dictionary<string, int>>(){
        {"Sword", new Dictionary<string, int>(){ {"demand", 9}, {"cost", 28} }},
        {"Axe", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 20} }},
        {"Shield", new Dictionary<string, int>(){ {"demand", 5}, {"cost", 18} }}
    }}
    };


//Print each inventory for each step
static void printingdict(Dictionary<string,Dictionary<string,int>> inv)
{
    foreach(var item in inv)
    {
        foreach(var temp in inv[item.Key])
        {
            Debug.Log(item.Key +" "+temp.Key+" "+temp.Value);
        }
    }
    Debug.Log("----------------------------------");
}

//Printing final version of q-table
static void printingqtable(double[,] qtable)
{
    int rowLength = qtable.GetLength(0);
    int colLength = qtable.GetLength(1);

    for (int a = 0; a < rowLength; a++)
    {
        for (int j = 0; j < colLength; j++)
        {
            Debug.Log(string.Format("{0}\t", q_table[a, j]));
        }
        Debug.Log(" ");
    }

}

//Deciding and sending action to the AI merchant
static int EpsilonGreedyPolicy(int state, double epsilon, double[,] q_table)
{

    int[] actions = new int[] { 0, 1, 2, 3, 4, 5, 6 };
    
    
    if (Random.Range(0 , 1) < epsilon)
    {
        // Explore - Choose a random action
        return actions[Random.Range(0, actions.Length)];
    }
    else
    {
        // Exploit - Choose the action with the highest Q-value
        double[] qtablestate = new double[7];
        for(int i = 0; i <= 6; i++)
        {
            qtablestate[i] = q_table[state,i];


        }
        qtablestate[5] = 12;
        double maxvalue = qtablestate.Max();
        int maxIndex = qtablestate.ToList().IndexOf(maxvalue);
        return maxIndex;
    }
}


public static void makeDecision()
{
    int num_episodes = 10;

// Set the learning parameters
    float learning_rate = 0.8f;
    float discount_factor = 0.1f;
    int inventory_limit = 10;

    //AI merchant starts learning trade
    for (int episode = 0; episode < num_episodes; episode++) 
{

    int agent_current_money = 50;
    
    // Initializing inventory each episode
    Dictionary<string, Dictionary<string, int>> agent_inventory = new Dictionary<string, Dictionary<string, int>>();
    agent_inventory.Add("Sword", new Dictionary<string, int>() { { "Exist", 0 }, { "cost", 14 } });
    agent_inventory.Add("Axe", new Dictionary<string, int>() { { "Exist", 0 }, { "cost", 14 } });
    agent_inventory.Add("Shield", new Dictionary<string, int>() { { "Exist", 0 }, { "cost", 14 } });
    
    //It holds total number of items
    int inventory_count = 0;

    //Defining initial state
    int agent_state = states["MineEmpty"];
    int next_agent_state = 0;
    
    //Defining variables
    double reward = 0;
    int i = 0;
    bool done = false;
    double epsilon = 1;
    string currentlocation = "Mine";

    //After 50 episodes AI merchants stops learning and starts trading according to q-table
    if (episode == 50)
    {
        epsilon = 0;
    }
    
    //Npc taking actions in each iteration
    while (!done)
    {

        int action = EpsilonGreedyPolicy(agent_state, epsilon, q_table);
        
        //Epsilon is decrease for each iteration
        epsilon -= 0.001;

        if (action == acts["goMine"])
        {
            //If current location is equeals to next location agent get penalty
            if (agent_state == states["MineEmpty"] || agent_state == states["MineHalfFull"] || agent_state == states["MineFull"])
            {
                reward = -9;
                next_agent_state = agent_state;
            }
            
            //If inventory is empty agent's reward is calculated according to buying action
            else if (inventory_count == 0)
            {
                Dictionary<string, Dictionary<string, double>> buyr_table = actions.CalculateBuy(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in buyr_table["Mine"])
                {
                    if (buyr_table["Mine"][item.Key] >= reward)
                    {
                        reward = buyr_table["Mine"][item.Key];
                    }
                }
                next_agent_state = states["MineEmpty"];
                currentlocation = "Mine";
            }
            
            //If inventory is not empty and not full reward is calculated according to buying and selling action, and highest reward is chosen
            else if (inventory_count > 0 && inventory_count < inventory_limit)
            {
                var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);
                double max = 0;
                reward = -100;
                foreach (var item in tuple.Item1["Mine"])
                {
                    if (tuple.Item1["Mine"][item.Key] >= reward)
                    {
                        reward = tuple.Item1["Mine"][item.Key];
                    }
                }

                next_agent_state = states["MineHalfFull"];
                currentlocation = "Mine";
            }
            
            // If inventory is full agent's reward is calculated according to selling action
            else if (inventory_count == inventory_limit)
            {
                var sellrtable = actions.CalculateSell(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in sellrtable["Mine"])
                {
                    if (sellrtable["Mine"][item.Key] >= reward)
                    {
                        reward = sellrtable["Mine"][item.Key];
                    }
                }

                next_agent_state = states["MineFull"];
                currentlocation = "Mine";
            }
            
            
        }
        
        //Movement actions are same with go mine action
        else if (action == acts["goShopA"])
        {
            if (agent_state == states["AEmpty"] || agent_state == states["AHalfFull"] || agent_state == states["AFull"])
            {
                reward = -9;
                next_agent_state = agent_state;
            }
            else if (inventory_count == 0)
            {
                Dictionary<string, Dictionary<string, double>> buyr_table = actions.CalculateBuy(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in buyr_table["A"])
                {
                    if (buyr_table["A"][item.Key] >= reward)
                    {
                        reward = buyr_table["A"][item.Key];
                    }
                }
                next_agent_state = states["AEmpty"];
                currentlocation = "A";
                // buy part
            }
            else if (inventory_count > 0 && inventory_limit < 10)
            {
                var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in tuple.Item1["A"])
                {
                    if (tuple.Item1["A"][item.Key] >= reward)
                    {
                        reward = tuple.Item1["A"][item.Key];
                    }
                }
                next_agent_state = states["AHalfFull"];
                currentlocation = "A";
            }
            else if (inventory_count == inventory_limit)
            {
                Dictionary<string, Dictionary<string, double>> sellrtable = actions.CalculateSell(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in sellrtable["A"])
                {
                    if (sellrtable["A"][item.Key] >= reward)
                    {
                        reward = sellrtable["A"][item.Key];
                    }
                }
                next_agent_state = states["AFull"];
                currentlocation = "A";
                // sell part
            }

        }

        else if (action == acts["goShopB"])
        {
            if (agent_state == states["BEmpty"] || agent_state == states["BHalfFull"] || agent_state == states["BFull"])
            {
                reward = -9;
                next_agent_state = agent_state;
            }
            else if (inventory_count == 0)
            {
                Dictionary<string, Dictionary<string, double>> buyr_table = actions.CalculateBuy(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in buyr_table["B"])
                {
                    if (buyr_table["B"][item.Key] >= reward)
                    {
                        reward = buyr_table["B"][item.Key];
                    }
                }
                next_agent_state = states["BEmpty"];
                currentlocation = "B";
                // buy part
            }
            else if (inventory_count > 0 && inventory_limit < 10)
            {
                var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in tuple.Item1["B"])
                {
                    if (tuple.Item1["B"][item.Key] >= reward)
                    {
                        reward = tuple.Item1["B"][item.Key];
                    }
                }
                next_agent_state = states["BHalfFull"];
                currentlocation = "B";
            }
            else if (inventory_count == inventory_limit)
            {
                Dictionary<string, Dictionary<string, double>>  sellrtable = actions.CalculateSell(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in sellrtable["B"])
                {
                    if (sellrtable["B"][item.Key] >= reward)
                    {
                        reward = sellrtable["B"][item.Key];
                    }
                }
                next_agent_state = states["BFull"];
                currentlocation = "B";
                // sell part
            }

        }

        else if (action == acts["goShopC"])
        {
            if (agent_state == states["CEmpty"] || agent_state == states["CHalfFull"] || agent_state == states["CFull"])
            {
                reward = -9;
                next_agent_state = agent_state;
            }
            else if (inventory_count == 0)
            {
                Dictionary<string, Dictionary<string, double>>  buyr_table = actions.CalculateBuy(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;

                foreach (var item in buyr_table["C"])
                {
                    if (buyr_table["C"][item.Key] >= reward)
                    {
                        reward = buyr_table["C"][item.Key];
                    }
                }

                next_agent_state = states["CEmpty"];
                currentlocation = "C";
                // buy part
            }
            else if (inventory_count > 0 && inventory_count < inventory_limit)
            {
                var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;

                foreach (var item in tuple.Item1["C"])
                {
                    if (tuple.Item1["C"][item.Key] >= reward)
                    {
                        reward = tuple.Item1["C"][item.Key];
                    }
                }

                next_agent_state = states["CHalfFull"];
                currentlocation = "C";
            }
            else if (inventory_count == inventory_limit)
            {
                Dictionary<string, Dictionary<string, double>>  sellrtable = actions.CalculateSell(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;

                foreach (var item in sellrtable["C"])
                {
                    if (sellrtable["C"][item.Key] >= reward)
                    {
                        reward = sellrtable["C"][item.Key];
                    }
                }

                next_agent_state = states["CFull"];
                currentlocation = "C";
            }

        }

        else if (action == acts["goMilitary"])
        {
            if (agent_state == states["MilitaryEmpty"] || agent_state == states["MilitaryHalfFull"] || agent_state == states["MilitaryFull"])
            {
                reward = -9;
                next_agent_state = agent_state;
            }
            else if (inventory_count == 0)
            {
                Dictionary<string, Dictionary<string, double>>  buyr_table = actions.CalculateBuy(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in buyr_table["Military"])
                {
                    if (buyr_table["Military"][item.Key] >= reward)
                    {
                        reward = buyr_table["Military"][item.Key];
                    }
                }
                next_agent_state = states["MilitaryEmpty"];
                currentlocation = "Military";
                // buy part
            }
            else if (inventory_count > 0 && inventory_count < inventory_limit)
            {
                var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in tuple.Item1["Military"])
                {
                    if (tuple.Item1["Military"][item.Key] >= reward)
                    {
                        reward = tuple.Item1["Military"][item.Key];
                    }
                }
                next_agent_state = states["MilitaryHalfFull"];
                currentlocation = "Military";
            }
            else if (inventory_count == inventory_limit)
            {
                Dictionary<string, Dictionary<string, double>>  sellrtable = actions.CalculateSell(agent_inventory, supply_table, demand_table, currentlocation);
                reward = -100;
                foreach (var item in sellrtable["Military"])
                {
                    if (sellrtable["Military"][item.Key] >= reward)
                    {
                        reward = sellrtable["Military"][item.Key];
                    }
                }
                next_agent_state = states["MilitaryFull"];
                currentlocation = "Military";
            }
        }
        
        
        
        else if (action == acts["Buy"])
        {
            // If there is empty space in inventory, AI merchant can buy items
            if (inventory_count <= inventory_limit && inventory_count >= 0)
            {
                // If inventory of AI is fully empty, reward is calculated according to buy function in action library
                if (agent_state == states["MineEmpty"] || agent_state == states["AEmpty"] || agent_state == states["BEmpty"] || agent_state == states["CEmpty"] || agent_state == states["MilitaryEmpty"])
                {
                    Dictionary<string, Dictionary<string, double>>  buyr_table = actions.CalculateBuy(agent_inventory, supply_table, demand_table, currentlocation);
                    reward = -100;
                    string itemName = "none";
                    
                    //finding hightest reward between items
                    foreach (var item in buyr_table[currentlocation])
                    {
                        if (buyr_table[currentlocation][item.Key] + 1 >= reward)
                        {
                            reward = buyr_table[currentlocation][item.Key] + 2;
                            itemName = item.Key;
                        }
                    }
                    
                    //making calculations updating values for buying action
                    agent_inventory[itemName]["cost"] = supply_table[currentlocation][itemName]["cost"];
                    agent_inventory[itemName]["Exist"] += 1;
                    agent_current_money = agent_current_money - supply_table[currentlocation][itemName]["cost"];
                    inventory_count = inventory_count + 1;
                    
                    if (inventory_count < inventory_limit)
                    {
                        next_agent_state = states[currentlocation + "HalfFull"];
                    }
                    
                    else if (inventory_count == inventory_limit)
                    {
                        next_agent_state = states[currentlocation + "Full"];
                    }
                }
                
                // If inventory of AI is not fully empty, reward is calculated according to buy and sell function in action library highest reward is chosen
                else if (agent_state == states["MineHalfFull"] || agent_state == states["AHalfFull"] || agent_state == states["BHalfFull"] || agent_state == states["CHalfFull"] || agent_state == states["MilitaryHalfFull"])
                {
                    // CalcHalf function returns highest value of buying or selling action
                    var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);

                    if (tuple.Item2 == "buy")
                    {
                        reward = -100;
                        string itemName = "none";
                        
                        //finding hightest reward between items
                        foreach (var item in tuple.Item1[currentlocation])
                        {
                            if (tuple.Item1[currentlocation][item.Key] >= reward)
                            {
                                reward = tuple.Item1[currentlocation][item.Key] + 2;
                                itemName = item.Key;
                            }
                        }
                        
                        //making calculations updating values for buying action
                        agent_inventory[itemName]["cost"] = supply_table[currentlocation][itemName]["cost"];
                        agent_inventory[itemName]["Exist"] += 1;
                        agent_current_money = agent_current_money - supply_table[currentlocation][itemName]["cost"];
                        inventory_count = inventory_count + 1;

                        if (inventory_count < inventory_limit)
                        {
                            next_agent_state = states[currentlocation + "HalfFull"];
                        }
                        else if (inventory_count == inventory_limit)
                        {
                            next_agent_state = states[currentlocation + "Full"];
                        }
                    }
                    else if (tuple.Item2 == "sell" && inventory_count != 0)
                    {
                        action = acts["Sell"];
                        reward = -100;
                        string itemName = "none";
                        
                        //finding hightest reward between items
                        foreach (var item in tuple.Item1[currentlocation])
                        {
                            if (tuple.Item1[currentlocation][item.Key] >= reward)
                            {
                                reward = tuple.Item1[currentlocation][item.Key] + 2;
                                itemName = item.Key;
                            }
                        }
                        
                        //making calculations updating values for buying action
                        if (agent_inventory[itemName]["Exist"] > 0)
                        {
                            agent_inventory[itemName]["Exist"] -= 1;
                            agent_current_money = agent_current_money + demand_table[currentlocation][itemName]["cost"];
                            inventory_count = inventory_count - 1;
                            next_agent_state = states[currentlocation + "HalfFull"];
                        }
                        else
                        {
                            reward = 0;
                            next_agent_state = agent_state;
                                }
                    }
                }
            }
        }

        else if (action == acts["Sell"])
        {
            // if there is item in the inventory AI merchant can sell the items
            if (inventory_count > 0 || inventory_count <= inventory_limit)
            {
                // If inventory if full, just sell action is calculated, AI merchant cannot buy items
                if (agent_state == states["MineFull"] || agent_state == states["AFull"] || agent_state == states["BFull"] || agent_state == states["CFull"] || agent_state == states["MilitaryFull"])
                {
                    Dictionary<string, Dictionary<string, double>>  sellrtable = actions.CalculateSell(agent_inventory, supply_table, demand_table, currentlocation);

                    reward = -100;
                    string itemName = "none";
                    
                    //finding hightest reward between items
                    foreach (var item in sellrtable[currentlocation])
                    {
                        if (sellrtable[currentlocation][item.Key] >= reward)
                        {
                            reward = sellrtable[currentlocation][item.Key] + 2;
                            itemName = item.Key;
                        }
                    }
                    
                    //making calculations updating values for selling action
                    if (agent_inventory[itemName]["Exist"] > 0)
                    {
                        agent_inventory[itemName]["Exist"] -= 1;
                        agent_current_money = agent_current_money + demand_table[currentlocation][itemName]["cost"];
                        inventory_count = inventory_count - 1;
                        next_agent_state = states[currentlocation + "HalfFull"];
                    }
                    else
                    {
                        reward = 0;
                        next_agent_state = agent_state;
                    }
                }
                
                // If inventory of AI is not fully empty, reward is calculated according to buy and sell function in action library highest reward is chosen
                else if (agent_state == states["MineHalfFull"] || agent_state == states["AHalfFull"] || agent_state == states["BHalfFull"] || agent_state == states["CHalfFull"] || agent_state == states["MilitaryHalfFull"])
                {
                    var tuple = actions.CalcHalf(agent_inventory, supply_table, demand_table, currentlocation);

                    if (tuple.Item2 == "sell")
                    {
                        reward = -100;
                        string itemName = "none";
                        
                        //finding hightest reward between items
                        foreach (var item in tuple.Item1[currentlocation])
                        {
                            if (tuple.Item1[currentlocation][item.Key] >= reward)
                            {
                                reward = tuple.Item1[currentlocation][item.Key] + 2;
                                itemName = item.Key;
                            }
                        }
                        
                        //making calculations updating values for selling action
                        if (agent_inventory[itemName]["Exist"] > 0)
                        {
                            agent_inventory[itemName]["Exist"] -= 1;
                            agent_current_money = agent_current_money + demand_table[currentlocation][itemName]["cost"];
                            inventory_count = inventory_count - 1;
                            next_agent_state = states[currentlocation + "HalfFull"];
                            if (inventory_count != 0 && inventory_count < inventory_limit)
                            {
                                next_agent_state = states[currentlocation + "HalfFull"];
                            }
                            else if (inventory_count == 0)
                            {
                                next_agent_state = states[currentlocation + "Empty"];
                            }
                        }
                        else
                        {
                            reward = 0;
                            next_agent_state = agent_state;
                        }
                    }
                    else if (tuple.Item2 == "buy" && inventory_count != 10)
                    {
                        action = acts["Buy"];
                       
                        if (tuple.Item2 == "buy")
                        {
                            reward = -100;
                            string itemName = "none";
                            
                            //finding hightest reward between items
                            foreach (var item in tuple.Item1[currentlocation])
                            {
                                if (tuple.Item1[currentlocation][item.Key] >= reward)
                                {
                                    reward = tuple.Item1[currentlocation][item.Key] + 2;
                                    itemName = item.Key;
                                }
                            }
                            
                            //making calculations updating values for buying action
                            agent_inventory[itemName]["cost"] = supply_table[currentlocation][itemName]["cost"];
                            agent_inventory[itemName]["Exist"] += 1;
                            agent_current_money -= supply_table[currentlocation][itemName]["cost"];
                            inventory_count++;

                            if (inventory_count < inventory_limit)
                            {
                                next_agent_state = states[currentlocation + "HalfFull"];
                            }
                            else if (inventory_count == inventory_limit)
                            {
                                next_agent_state = states[currentlocation + "Full"];
                            }
                            else if (agent_state == states["MineEmpty"] || agent_state == states["AEmpty"] || agent_state == states["BEmpty"] || agent_state == states["CEmpty"] || agent_state == states["MilitaryEmpty"])
                            {
                                reward = -100;
                                next_agent_state = agent_state;
                            }
                        }
                    }
                }
            }

        }
        
        //updating q table
        q_table[agent_state, action] = q_table[agent_state, action] + learning_rate * (reward + discount_factor * q_table[next_agent_state, action] - q_table[agent_state, action]);
        agent_state = next_agent_state;
        
        // after 1000 iteration the episode is finished
        i = i + 1;
        if (i == 1000)
        {
            done = true;
        }
        Debug.Log("step: " + i + "\ncurrent money: " + agent_current_money + "\ncurrent location: " +currentlocation);
        printingdict(agent_inventory);
        //Thread.Sleep(1);
    }
    printingqtable(q_table);
}
}
}


