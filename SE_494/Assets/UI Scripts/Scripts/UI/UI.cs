using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{

    public Controller controller;

    List<Item> inventoryItems = new List<Item>();
    List<Item> shopItems = new List<Item>();
    List<Item> allItems = new List<Item>();
    public List<supplyItems> supplyItems = new List<supplyItems>();





    public VisualElement root;
    public VisualElement dragZ;
    public VisualElement currentItem;
    public VisualElement soldItem;
    public VisualElement boughtItem;
    public Label pathLabel;
    public VisualElement temp;
    public VisualElement screen;

    public int randomnumber;

    

    public bool isOpen;


    private int num;
    public int bag;



    private void OnEnable()
    {


        Items.additems();
        for (int i = 0; i < 5; i++)
        {
            shopItems.Add(Resources.Load<Item>(Items.strings[UnityEngine.Random.Range(1,10)]));
        }
        for (int i = 5; i < 10; i++)
        {
            inventoryItems.Add(Resources.Load<Item>(Items.strings[UnityEngine.Random.Range(1, 10)]));
        }
        //GetInventoryList(inventoryItems);
        //GetShopList(shopItems);
        GetAllItems(allItems);
        GetSupplyItems(supplyItems);
        root = GetComponent<UIDocument>().rootVisualElement;
        dragZ = root.Q("DragArea");


        Shop_Window shopWindow = new Shop_Window();
        Drag_Area drag_Area = new Drag_Area();
        Buy_Sell buy_Sell = new Buy_Sell();

        
        shopWindow.cancel += () =>
        {
            screen.style.display = DisplayStyle.None;
            isOpen= false;
            // root.Remove(shopWindow);
        };

        shopWindow.OpenBuyTransactionEvent += () =>
        {

            root.Add(buy_Sell);
            buy_Sell.itemName.text = Setup.dummyDataShop;
            for (int i = 0;i < allItems.Count; i++)
            {
                if (allItems[i].name==Setup.dummyDataShop)
                {
                    buy_Sell.supplyNumber.text = allItems[i].supply.ToString();
                    buy_Sell.demandNumber.text = allItems[i].demand.ToString();

                    if (shopItems.Contains(allItems[i]))
                    {
                        buy_Sell.inShopNumber.text = Setup.dummyAmount;
                    }
                    else
                        buy_Sell.inShopNumber.text = "0";

                    if (inventoryItems.Contains(allItems[i]))
                    {
                        buy_Sell.inInventoryNumber.text = Setup.dummyAmount;
                         
                    }
                    else
                        buy_Sell.inInventoryNumber.text = "0";
                }
            }

            



           // buy_Sell.buyButton.text = "Buy";
            Setup.ShowSupply(root, Setup.dummyList);
        };

        shopWindow.OpenSellTransactionEvent += () =>
        {

            root.Add(buy_Sell);
            buy_Sell.itemName.text = Setup.dummyDataInv;

            for (int i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].name == Setup.dummyDataInv)
                {
                    buy_Sell.supplyNumber.text = allItems[i].supply.ToString();
                    buy_Sell.demandNumber.text = allItems[i].demand.ToString();

                    if (shopItems.Contains(allItems[i]))
                    {
                        buy_Sell.inShopNumber.text = Setup.dummyAmount;
                    }
                    else
                        buy_Sell.inShopNumber.text = "0";


                    if (inventoryItems.Contains(allItems[i]))
                    {
                        buy_Sell.inInventoryNumber.text = Setup.dummyAmount;
                    }
                    else
                        buy_Sell.inInventoryNumber.text = "0";

                }
            }
            //buy_Sell.buyButton.text = "Sell";
            Setup.ShowSupply(root, supplyItems);

        };

        buy_Sell.leftarrow += () =>
        {
            int diff;
            int.TryParse(Setup.dummyAmount, out diff);
            if (diff < num)
            {
                StartCoroutine(fail());
            }

            else
            {
               
                    for (int i = 0; i < shopWindow.shopCount; i++)
                    {

                        soldItem = root.Query<Button>(className: "slotSelector").Name("ShopItem" + i).Children<VisualElement>().AtIndex(0);
                        soldItem = soldItem.Q("Shop_Icon");


                        if (soldItem.style.backgroundImage.ToString() == "Null")
                        {
                            soldItem.style.backgroundImage = Resources.Load<Texture2D>(Setup.dummyPath);
                            


                            Label dummy = new Label();
                            dummy = soldItem.Q<Label>("Shop_Amount");
                            dummy.text = num.ToString();
                            dummy.userData= num.ToString();

                            int bbb;
                            int.TryParse(Setup.dummyAmount, out bbb);
                            Setup.dummyAmount = (bbb - num).ToString();

                            if (Setup.dummyAmount == "0")
                            {
                                temp = root.Q(Setup.dummyPlace);
                                temp.userData = null;
                                temp = temp.Q("Inventory_Icon");
                                //temp.userData = null;
                                temp.style.backgroundImage = null;
                            }


                            temp = root.Query(Setup.dummyPlace).Children<VisualElement>().AtIndex(0);
                            soldItem = root.Query<Button>(className: "slotSelector").Name("ShopItem" + i).Children<VisualElement>().AtIndex(0);
                            soldItem.userData=temp.userData;


                            soldItem.Q("Shop_Icon").userData = temp.Q("Inventory_Icon").userData.ToString();
                            soldItem.Q("Amount").userData = Setup.invPrice;



                            Label tempLabel = new Label();
                            tempLabel = temp.Q<Label>("Inventory_Amount");
                            tempLabel.text = Setup.dummyAmount;
                            tempLabel.userData = Setup.dummyAmount;

                            if (Setup.dummyAmount == "0")
                            {
                                tempLabel.text = null;

                            }

                            for(int j = 0; j < inventoryItems.Count-1; j++)
                            {
                                if (inventoryItems[j].item_Name == Setup.dummyDataInv)
                                {
                                    shopItems.Add(inventoryItems[j]);

                                    if (Setup.dummyAmount == "0")
                                        inventoryItems.RemoveAt(j);
                                }
                            }
                            StartCoroutine(correct());


                            string cal = Setup.invPrice;

                            int ccc;
                            int.TryParse(cal, out ccc);

                            Setup.final += num * ccc;
                            shopWindow.moneyNumber.text=Setup.final.ToString();
                            soldItem.AddManipulator(new ShopSelector(root));

                            break;


                        }
                    }

                root.Remove(buy_Sell);
                num = 0;
                buy_Sell.amount.text = "0";
            }
         };


        buy_Sell.rightarrow += () => 
        {
            int diff;
            int.TryParse(Setup.dummyAmount, out diff);
            if (diff < num)
            {
                StartCoroutine(fail());
            }
            else
            {



                for (int i = 0; i < shopWindow.inventoryCount; i++)
                {

                    boughtItem = root.Query<Button>(className: "slotSelector").Name("Item" + i).Children<VisualElement>().AtIndex(0);
                    boughtItem = boughtItem.Q("Inventory_Icon");


                    if (boughtItem.style.backgroundImage.ToString() == "Null")
                    {
                        boughtItem.style.backgroundImage = Resources.Load<Texture2D>(Setup.dummyPath);

                        Label dummy = new Label();
                        dummy = boughtItem.Q<Label>("Inventory_Amount");
                        dummy.text = num.ToString();
                        dummy.userData = num.ToString();


                        int aaa;
                        int.TryParse(Setup.dummyAmount, out aaa);
                        Setup.dummyAmount = (aaa - num).ToString();

                        if (Setup.dummyAmount == "0")
                        {
                            temp = root.Q(Setup.dummyPlace);
                            temp.userData = null;
                            temp = temp.Q("Shop_Icon");
                            //temp.userData = null;
                            temp.style.backgroundImage = null;
                        }

                        temp = root.Query(Setup.dummyPlace).Children<VisualElement>().AtIndex(0);
                        boughtItem = root.Query<Button>(className: "slotSelector").Name("Item" + i).Children<VisualElement>().AtIndex(0);
                        boughtItem.userData = temp.userData;

                        boughtItem.Q("Inventory_Icon").userData = temp.Q("Shop_Icon").userData.ToString();

                        boughtItem.Q("Amount").userData = Setup.shopPrice;



                        Label tempLabel = new Label();
                        tempLabel = temp.Q<Label>("Shop_Amount");
                        tempLabel.text = Setup.dummyAmount;
                        tempLabel.userData = Setup.dummyAmount;

                        if (Setup.dummyAmount == "0")
                        {
                            tempLabel.text = null;

                        }


                        for (int j = 0; j < shopItems.Count - 1; j++)
                        {
                            if (shopItems[j].item_Name == Setup.dummyDataShop)
                            {
                                inventoryItems.Add(shopItems[j]);

                                if (Setup.dummyAmount == "0")
                                    shopItems.RemoveAt(j);
                            }
                        }

                        StartCoroutine(correct());


                        string cal = Setup.shopPrice;

                        int ddd;
                        int.TryParse(cal, out ddd);
                        Setup.final -= num * ddd;
                        shopWindow.moneyNumber.text = Setup.final.ToString();
                        boughtItem.AddManipulator(new InventorySelector(root));

                        break;
                    }

                }
                root.Remove(buy_Sell);
                num = 0;
                buy_Sell.amount.text = "0";
            }
        };
       

        buy_Sell.closeTransaction += () =>

        {
            root.Remove(buy_Sell);
            num = 0;
            buy_Sell.amount.text = "0";
        };



        buy_Sell.increase += () =>
       {
           num++;
           buy_Sell.amount.text = num.ToString();

       };

        buy_Sell.decrease += () =>
        {
            num--;
            buy_Sell.amount.text = num.ToString();
        };



        Initialize(shopWindow, drag_Area);

        shopWindow.style.display = DisplayStyle.None;
        drag_Area.style.display = DisplayStyle.None;

        screen = root.Query(className: "shop_window_container");


    }

    private void Update()
    {
        // Button shop;
        // shop = root.Q<Button>("Shop_A");
        // shop.clicked += () =>
        // {
        //     screen.style.display = DisplayStyle.Flex;
        //     isOpen = true;
        //     return;
        // };


        screen = root.Query(className: "shop_window_container");
        Buy_Sell buy;
        buy = root.Q<Buy_Sell>(className: "popup_container");
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isOpen)
            {
                screen.style.display = DisplayStyle.Flex;
                isOpen = true;
                return;
            }

            screen.style.display = DisplayStyle.None;
           // root.Remove(buy);
            isOpen = false;
        }

        //if (Input.GetKeyDown(KeyCode.A))

        //    Debug.Log(shopItems.Count);

        //if (Input.GetKeyDown(KeyCode.S))


    }

    public void Initialize(Shop_Window shop, Drag_Area drag)
    {
        root.Add(shop);
        root.Add(drag);
        //Setup.InitializeDragDrop(root);
        Setup.InitializeInventoryItems(root, inventoryItems);
        Setup.InitializeShopItems(root, shopItems);
        Setup.GiveSelectorInvItems(root, inventoryItems);
        Setup.GiveSelectorShopItems(root, shopItems);
    }

    public void GetInventoryList(List<Item> list)
    {
        list.AddRange(Resources.LoadAll<Item>("inventoryItems"));
    }
    public void GetShopList(List<Item> list)
    {
        list.AddRange(Resources.LoadAll<Item>("shopItems"));
    }
    public void GetAllItems(List<Item> list)
    {
        list.AddRange(Resources.LoadAll<Item>("allItems"));
    }

    public void GetSupplyItems(List<supplyItems> list)
    {
        list.AddRange(Resources.LoadAll<supplyItems>("supply"));
    }



    IEnumerator correct()
    {
        Label correct = new Label();
        correct = root.Q<Label>("Correct");
        correct.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(1);
        correct.style.display = DisplayStyle.None;

    }
    IEnumerator fail()
    {
        Label correct = new Label();
        correct = root.Q<Label>("Fail");
        correct.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(1);
        correct.style.display = DisplayStyle.None;

    }


}
