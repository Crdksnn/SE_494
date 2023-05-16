using System;
using System.Collections.Generic;


// action library calculates reward for each item in a shop and returns reward table of items
namespace ConsoleApp1
{
    internal class actions
    {
        // buying rewards are calculated for each item in each shops
        public static Dictionary<string, Dictionary<string, double>> reward_table_buy = new Dictionary<string, Dictionary<string, double>>()
{
    {"Mine", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"A", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"B", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"C", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"Military", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}}
};

        // selling rewards are calculated for each item in each shops
       public static Dictionary<string, Dictionary<string, double>> reward_table_sell = new Dictionary<string, Dictionary<string, double>>()
{
    {"Mine", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"A", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"B", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"C", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}},
    {"Military", new Dictionary<string, double>() {{"Sword", 0}, {"Axe", 0}, {"Shield", 0}}}
};
        
       // CalculateBuy function calculates item rewards for buying action
        public static Dictionary<string, Dictionary<string, double>> CalculateBuy(Dictionary<string, Dictionary<string, int>> cost,
                                                                                    Dictionary<string, Dictionary<string, Dictionary<string, int>>> supply,
                                                                                    Dictionary<string, Dictionary<string, Dictionary<string, int>>> demand,
                                                                                    string loc)
        {
            // items' reward are calculated based on supply demand ratio and distance between shops
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, int>>> city in supply)
            {
                foreach (KeyValuePair<string, Dictionary<string, int>> item in supply[city.Key])
                {
                    reward_table_buy[city.Key][item.Key] = (supply[city.Key][item.Key]["supply"] / demand[city.Key][item.Key]["demand"]);
                }
            }

            // calc_with_distance(loc, location_table);
            // Console.WriteLine("buy: " + loc + ", " + reward_table_buy);
            return reward_table_buy;
        }
        
        // CalculateSell function calculates item rewards for selling action
        public static Dictionary<string, Dictionary<string, double>> CalculateSell(Dictionary<string, Dictionary<string, int>> cost,
                                                                                    Dictionary<string, Dictionary<string, Dictionary<string, int>>> supply,
                                                                                    Dictionary<string, Dictionary<string, Dictionary<string, int>>> demand,
                                                                                    string loc)
        {
            Dictionary<string, Dictionary<string, double>> rewardTableSell = new Dictionary<string, Dictionary<string, double>>();
            
            // items' reward are calculated based on supply demand ratio and distance between shops
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, int>>> city in supply)
            {
                rewardTableSell[city.Key] = new Dictionary<string, double>();

                foreach (KeyValuePair<string, Dictionary<string, int>> item in supply[city.Key])
                {
                    rewardTableSell[city.Key][item.Key] = demand[city.Key][item.Key]["demand"] / supply[city.Key][item.Key]["supply"];
                }
            }

            // calc_with_distance(loc, location_table)
            // Console.WriteLine("sell: " + loc + " " + string.Join(", ", rewardTableSell));
            return rewardTableSell;
        }
        
        // distance between shops are calculted and added in reward value
        public void calc_with_distance(string location, Dictionary<string, int> location_table)
        {
            foreach (var city in reward_table_buy)
            {
                foreach (var item in reward_table_buy[city.Key])
                {
                    double distance = Math.Abs(location_table[city.Key] - location_table[location]) + 1;
                    reward_table_buy[city.Key][item.Key] /= distance * 0.3;
                }
            }

            foreach (var city in reward_table_sell)
            {
                foreach (var item in reward_table_sell[city.Key])
                {
                    double distance = Math.Abs(location_table[city.Key] - location_table[location]) + 1;
                    reward_table_sell[city.Key][item.Key] /= distance * 0.3;
                }
            }
        }


        // If inventory of AI is not fully empty, reward is calculated according to buy and sell function in action library highest reward is chosen
        public static Tuple<Dictionary<string, Dictionary<string, double>>, string> CalcHalf(Dictionary<string, Dictionary<string, int>> inv, Dictionary<string, Dictionary<string, Dictionary<string, int>>> suptab, Dictionary<string, Dictionary<string, Dictionary<string, int>>> demtab, string curloc)
        {
            Dictionary<string, Dictionary<string, double>> sellState = CalculateSell(inv, suptab, demtab, curloc);
            double sellMax = -10;
            double buyMax = -10;

            foreach (var city in sellState.Keys)
            {
                foreach (var item in sellState[city].Keys)
                {
                    if (sellState[curloc][item] > sellMax)
                    {
                        sellMax = sellState[curloc][item];
                    }
                }
            }

            Dictionary<string, Dictionary<string, double>> buyState = CalculateBuy(inv, suptab, demtab, curloc);
            foreach (var city in buyState.Keys)
            {
                foreach (var item in buyState[city].Keys)
                {
                    if (buyState[curloc][item] > buyMax)
                    {
                        buyMax = buyState[curloc][item];
                    }
                }
            }

            if (sellMax > buyMax)
            {
                return new Tuple<Dictionary<string, Dictionary<string, double>>, string>(sellState, "sell");
            }
            else
            {
                return new Tuple<Dictionary<string, Dictionary<string, double>>, string>(buyState, "buy");
            }
        }



    }
}

