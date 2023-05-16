//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class Inventory_Window : VisualElement
//{
//    public new class UxmlFactory : UxmlFactory<Inventory_Window> { }



//    private const string Stylesheet = "Inventory_Window_USS";

//    private const string stylesheetContainer = "inventory_window_container";
//    private const string ussInventoryWindow = "inventory_window";
//    private const string ussInventoryText = "inventory_text";
//    private const string ussUpContainer = "up_container";
//    private const string ussScrollContainer = "scroll_container";
//    private const string ussScrollWindow = "scroll_window";
//    private const string ussScrollView = "scroll_view";



//    private ScrollView scrollView;


//    public Inventory_Window()
//    {
//        styleSheets.Add(Resources.Load<StyleSheet>(inventoryStylesheet));
//        AddToClassList(stylesheetContainer);

//        VisualElement window = new VisualElement();
//        window.name = "Inventory_Window";
//        hierarchy.Add(window);
//        window.AddToClassList(ussInventoryWindow);

//        VisualElement windowUp= new VisualElement();
//        windowUp.name = "Window_Up";
//        window.Add(windowUp);
//        windowUp.AddToClassList(ussInventoryUpContainer);

//        Label txt = new Label();
//        windowUp.Add(txt);
//        txt.text = "Inventory";
//        ColorUtility.TryParseHtmlString("#FFB400", out Color defaultcolor);
//        txt.style.color= defaultcolor;
//        txt.AddToClassList(ussInventoryText);

//        VisualElement windowBottom= new VisualElement();
//        windowBottom.name = "Window_Bottom";
//        window.Add(windowBottom);
//        windowBottom.AddToClassList(ussInventoryScrollContainer);

//        VisualElement scrollWindow= new VisualElement();
//        scrollWindow.name = "Scroll_Window";
//        windowBottom.Add(scrollWindow);
//        scrollWindow.AddToClassList(ussInventoryScrollWindow);

//        inventoryScrollView = new ScrollView();
//        inventoryScrollView.name = "ScrollView_Inventory";
//        scrollWindow.Add(inventoryScrollView);
//        inventoryScrollView.AddToClassList(ussInventoryScrollView);


//        InitializeContent();

//    }




//    public void InitializeContent()
//    {


//        for (int i = 0; i < 100; i++)
//        {
//            InventorySlot inventorySlot = new InventorySlot();
          
//            scrollView.contentContainer.Add(inventorySlot);
//            inventorySlot.name= "Item " + i.ToString();
                    
//        }
//    }
//}

