using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Setup
{

    public static string dummyPath;
    public static string dummyAmount;
    public static string dummyPlace;
    public static string dummyDataInv;
    public static string dummyDataShop;
    public static int final=100;
    public static string invPrice;
    public static string shopPrice;
    public static int dummyDemand;
    public static List<supplyItems> dummyList;


    public static void InitializeDragDrop(VisualElement root)//adds the IconDragger manipulator to the icons
    {
        root.Query<VisualElement>(className: "inventorySlotContainer")
            .Children<VisualElement>()
            .ForEach((elem) =>
            {
                elem.AddManipulator(new Item_Dragger(root));

            });
       
        root.Query<VisualElement>(className: "shopSlotContainer")
    .Children<VisualElement>()
    .ForEach((elem) =>
    {
        elem.AddManipulator(new Item_Dragger(root));

    });

    }

    public static void InitializeInventoryItems(VisualElement root, List<Item> items)
    {
        
        int currentItemIndex = 0;

        foreach(Item item in items)
        {
            

            VisualElement slot = root.Query<Button>(className: "slotSelector").Name("Item"+ currentItemIndex).Children<VisualElement>().AtIndex(0);
            slot.Q("Inventory_Icon").style.backgroundImage = Resources.Load<Texture2D>(item.img_path);
            slot.Q<Label>("Inventory_Amount").text=item.itemAmount.ToString();
            slot.userData = item.item_Name;
            slot.Q("Inventory_Icon").userData = item.img_path;
            slot.Q<Label>("Inventory_Amount").userData = item.itemAmount;
            slot.Q("Amount").userData = item.item_Price;
            slot.Q<Label>("Supply_Info").userData = item.supply;
            slot.Q<Label>("Demand_Info").userData = item.demand;

            currentItemIndex++;
        }
    }

    public static void InitializeShopItems(VisualElement root, List<Item> items)
    {

        int CurrentItemIndex = 0;

        foreach (Item item in items)
        {


            VisualElement slot = root.Query<Button>(className: "slotSelector").Name("ShopItem" + CurrentItemIndex).Children<VisualElement>().AtIndex(0);
            slot.Q("Shop_Icon").style.backgroundImage = Resources.Load<Texture2D>(item.img_path);
            slot.Q<Label>("Shop_Amount").text = item.itemAmount.ToString();

            slot.userData = item.item_Name;
            slot.Q("Shop_Icon").userData = item.img_path;
            slot.Q<Label>("Shop_Amount").userData = item.itemAmount;
            slot.Q("Amount").userData = item.item_Price;
            slot.Q<Label>("Supply_Info").userData = item.supply;
            slot.Q<Label>("Demand_Info").userData = item.demand;




            CurrentItemIndex++;
        }
    }

    //public static void InitializeShopItems(VisualElement root, Item item)
    //{

    //        VisualElement slot = root.Query<Button>(className: "slotSelector").Name("ShopItem"+index).Children<VisualElement>().AtIndex(0);
    //        slot = slot.Q("Shop_Icon");
    //    if (slot.style.backgroundImage.ToString()== "Null")
    //    {
    //        slot = root.Query<Button>(className: "slotSelector").Name("ShopItem" + index).Children<VisualElement>().AtIndex(0);
    //        slot.Q("Shop_Icon").style.backgroundImage = Resources.Load<Texture2D>(item.img_path);
    //        slot.Q<Label>("Shop_Amount").text = item.itemAmount.ToString();

    //        slot.userData = item.item_Name;
    //        slot.Q("Shop_Icon").userData = item.img_path;
    //        slot.Q<Label>("Shop_Amount").userData = item.itemAmount;
    //    }

    //}

    public static void GiveSelectorInvItems(VisualElement root, List<Item> items)
    {
        int index = 0;
        foreach (Item item in items)
        {
            VisualElement currentItem = root.Query<Button>(className: "slotSelector").Name("Item" + index).Children<VisualElement>().AtIndex(0);
            currentItem.AddManipulator(new InventorySelector(root));
            index++;
        }
    }

    public static void GiveSelectorShopItems(VisualElement root, List<Item> items)
    {
        int index = 0;
        foreach (Item item in items)
        {

               VisualElement currentItem = root.Query<Button>(className: "slotSelector").Name("ShopItem" + index).Children<VisualElement>().AtIndex(0);
                currentItem.AddManipulator(new ShopSelector(root));
            index++;
        }
    }


    public static void ShowSupply(VisualElement root, List<supplyItems> items)
    {

        int CurrentItemIndex = 0;

        foreach (supplyItems item in items)
        {


            VisualElement slot = root.Query<VisualElement>(className: "info_selector").Name("SupplyItem" + CurrentItemIndex).Children<VisualElement>().AtIndex(0);
            Label temp;
            temp = slot.Q<Label>("City");
            temp.text = item.city;

            temp = slot.Q<Label>("Supply");
            temp.text = item.supply;

            temp = slot.Q<Label>("Price");
            temp.text = item.price.ToString();

            temp = slot.Q<Label>("Distance");
            temp.text=item.distance.ToString();

            temp = slot.Q<Label>("Date");
            temp.text = item.date;

            CurrentItemIndex++;
        }
    }

    public static void ShowDemand(VisualElement root, List<supplyItems> items)
    {

        int CurrentItemIndex = 0;

        foreach (supplyItems item in items)
        {


            VisualElement slot = root.Query<VisualElement>(className: "info_selector").Name("DemandItem" + CurrentItemIndex).Children<VisualElement>().AtIndex(0);
            Label temp;
            temp = slot.Q<Label>("City");
            temp.text = item.city;

            temp = slot.Q<Label>("Supply");
            temp.text = item.supply;

            temp = slot.Q<Label>("Price");
            temp.text = item.price.ToString();

            temp = slot.Q<Label>("Distance");
            temp.text = item.distance.ToString();

            temp = slot.Q<Label>("Date");
            temp.text = item.date;

            CurrentItemIndex++;
        }
    }



}