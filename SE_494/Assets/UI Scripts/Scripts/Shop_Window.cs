using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Shop_Window : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Shop_Window> { }


    //shop uss
    private const string shopStylesheet = "Shop_Window_USS";
    private const string shopStylesheetContainer = "shop_window_container";

    private const string ussShopWindow = "shop_window";

    private const string ussShopUpContainer = "up_container";
    private const string ussShopText = "shop_text";
    private const string ussShopCancelButton = "cancel_button";

    private const string ussShopScrollContainer = "scroll_container";
    private const string ussShopScrollWindow = "scroll_window";
    private const string ussShopScrollView = "scroll_view";

    private const string ussShopBottomContainer = "bottom_container";
    private const string ussShopBottomButton = "bottom_button";
    //shop uss

    //inv uss
    private const string inventoryStylesheet = "Inventory_Window_USS";

    private const string inventoryStylesheetContainer = "inventory_window_container";
    private const string ussInventoryWindow = "inventory_window";
    private const string ussInventoryText = "inventory_text";
    private const string ussInventoryUpContainer = "up_container";
    private const string ussInventoryScrollContainer = "scroll_container";
    private const string ussInventoryScrollWindow = "scroll_window";
    private const string ussInventoryScrollView = "scroll_view";
    private const string ussMoney = "money";

    //inv uss

    private ScrollView shopScrollView;
    private ScrollView inventoryScrollView;

    public int shopCount;
    public int inventoryCount;

    public Label moneyNumber;

    public Shop_Window() 
    {
 

        //SHOP
        styleSheets.Add(Resources.Load<StyleSheet>(shopStylesheet));
        AddToClassList(shopStylesheetContainer);

        //main window
        VisualElement window = new VisualElement();
        window.name = "Shop_Window";
        hierarchy.Add(window);
        window.AddToClassList(ussShopWindow);

        //up container
        VisualElement upContainer = new VisualElement();
        upContainer.name = "Up_Container";
        window.Add(upContainer);
        upContainer.AddToClassList(ussShopUpContainer);

        Label shopText = new Label();
        shopText.text = "Shop";
        shopText.name = "Shop_Text";
        upContainer.Add(shopText);
        shopText.AddToClassList(ussShopText);

        Button closeButton = new Button();
        closeButton.name = "Close_Button";
        closeButton.AddToClassList(ussShopCancelButton);
        upContainer.Add(closeButton);

        //scroll container
        VisualElement centerContainer = new VisualElement();
        centerContainer.name = "Center_Container";
        window.Add(centerContainer);
        centerContainer.AddToClassList(ussShopScrollContainer);

        VisualElement scrollWindow = new VisualElement();
        scrollWindow.name = "Scroll_Window";
        centerContainer.Add(scrollWindow);
        scrollWindow.AddToClassList(ussShopScrollWindow);

        shopScrollView = new ScrollView();
        shopScrollView.name = "ScrollView_Shop";
        scrollWindow.Add(shopScrollView);
        shopScrollView.AddToClassList(ussShopScrollView);

        //bottom container
        //VisualElement bottomContainer = new VisualElement();
        //bottomContainer.name = "Bottom_Container";
        //window.Add(bottomContainer);
        //bottomContainer.AddToClassList(ussShopBottomContainer);

        //Button buyButton = new Button();
        //buyButton.name = "Buy_Button";
        //buyButton.text = "Buy";
        //bottomContainer.Add(buyButton);
        //buyButton.AddToClassList(ussShopBottomButton);


        closeButton.clicked += CancelWindow;

        ShopInitializeContent();
        //SHOP

        //INVENTORY
        styleSheets.Add(Resources.Load<StyleSheet>(inventoryStylesheet));
        //AddToClassList(inventoryStylesheetContainer);

        VisualElement inventoryWindow = new VisualElement();
        inventoryWindow.name = "Inventory_Window";
        hierarchy.Add(inventoryWindow);
        inventoryWindow.AddToClassList(ussInventoryWindow);

        VisualElement windowUp = new VisualElement();
        windowUp.name = "Window_Up";
        inventoryWindow.Add(windowUp);
        windowUp.AddToClassList(ussInventoryUpContainer);

        Label txt = new Label();
        windowUp.Add(txt);
        txt.text = "Inventory";
        ColorUtility.TryParseHtmlString("#FFB400", out Color defaultcolor);
        txt.style.color = defaultcolor;
        txt.AddToClassList(ussInventoryText);

        Label money = new Label();
        windowUp.Add(money);
        money.text= "Money: ";
        money.style.color = defaultcolor;
        money.AddToClassList(ussInventoryText);

        moneyNumber= new Label();
        moneyNumber.text = "100";
        hierarchy.Add(moneyNumber);
        moneyNumber.AddToClassList(ussMoney);
        


        VisualElement windowBottom = new VisualElement();
        windowBottom.name = "Window_Bottom";
        inventoryWindow.Add(windowBottom);
        windowBottom.AddToClassList(ussInventoryScrollContainer);

        VisualElement inventoryScrollWindow = new VisualElement();
        inventoryScrollWindow.name = "Scroll_Window";
        windowBottom.Add(inventoryScrollWindow);
        inventoryScrollWindow.AddToClassList(ussInventoryScrollWindow);

        inventoryScrollView = new ScrollView();
        inventoryScrollView.name = "ScrollView_Inventory";
        inventoryScrollWindow.Add(inventoryScrollView);
        inventoryScrollView.AddToClassList(ussInventoryScrollView);
        InventoryInitializeContent();
        //INVENTORY
    }



    public event Action cancel;
    public event Action OpenSellTransactionEvent;
    public event Action OpenBuyTransactionEvent;


    void CancelWindow()
    {
        cancel?.Invoke();
    }

    void OpenSellTransactionWindow()
    {
        OpenSellTransactionEvent?.Invoke();
    }

    void OpenBuyTransactionWindow()
    {
        OpenBuyTransactionEvent?.Invoke();
    }



    private void ShopInitializeContent()
    {


        for (shopCount = 0; shopCount < 10; shopCount++)
        {
            ShopSlot shopSlot = new ShopSlot();
            shopScrollView.contentContainer.Add(shopSlot);
            shopSlot.name = "ShopItem" + shopCount.ToString();
            shopSlot.clicked += OpenBuyTransactionWindow;
        }
    }
    private void InventoryInitializeContent()
    {


        for (inventoryCount = 0; inventoryCount < 10; inventoryCount++)
        {
            InventorySlot inventorySlot = new InventorySlot();
            inventoryScrollView.contentContainer.Add(inventorySlot);
            inventorySlot.name = "Item" + inventoryCount.ToString();
            inventorySlot.clicked += OpenSellTransactionWindow;
            
        }

    }

}
