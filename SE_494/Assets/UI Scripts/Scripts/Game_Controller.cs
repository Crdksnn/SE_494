using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;




public class Game_Controller : MonoBehaviour
{
    public VisualElement root;
    public VisualElement dragZ;

    public List<Item> InventoryItems = new List<Item>();
        ScrollView scrollViewInventory;
        ScrollView scrollViewShop;


    



    private void OnEnable()
    {
        
        GetList(InventoryItems);

        //Inventory_Window inventoryWindow = new Inventory_Window();
        Shop_Window shopWindow = new Shop_Window();
        Drag_Area drag_Area= new Drag_Area();
        Buy_Sell buy_Sell= new Buy_Sell();

        shopWindow.cancel += () =>
        {
            root.Remove(shopWindow);
            //root.Remove(inventoryWindow);
            
        };



        root = GetComponent<UIDocument>().rootVisualElement;
        dragZ = root.Q("DragArea");

        root.Add(buy_Sell);
        //OpenInventory(root,inventoryWindow);
        //OpenShop(root,shopWindow);
        //OpenDrag(root,drag_Area);
        
        //dragZ.PlaceInFront(shopWindow);

         //scrollViewInventory = root.Q<ScrollView>("ScrollView_Inventory");
          scrollViewShop = root.Q<ScrollView>("ScrollView_Shop");




        //Initialize();


        
    }



    private void Initialize()
    {
        Setup.InitializeDragDrop(root);
        Setup.InitializeInventoryItems(root, InventoryItems);    
    }



   private void OpenShop(VisualElement root, Shop_Window shop)
    {
        root.Add(shop);
    }

    private void OpenDrag(VisualElement root, Drag_Area area)
    {
        root.Add(area);
    }


    public void GetList(List<Item> list)
    {
        list.AddRange(Resources.LoadAll<Item>("Items"));
    }


}
