using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Buy_Sell : VisualElement
{

    public new class UxmlFactory : UxmlFactory<Buy_Sell> { }

    private const string stylesheet = "PopUp";

    private const string ussContainer = "popup_container";
    private const string ussPopUpWindow = "popup_window";

    private const string ussHorizontalContainer = "transaction_container";
    private const string ussTransactionText = "transaction_text";
    private const string ussCancelButton = "cancel_button";

    private const string ussItemNameContainer = "item_name_container";
    private const string ussItemNameText = "item_name_text";

    private const string ussAmountLayerContainer = "amount_layer_container";
    private const string ussAmount = "amount_buysell";

    private const string ussChangeAmountLayerContainer = "change_amount_layer_container";
    private const string ussChangeAmountPlus = "plus_button";
    private const string ussChangeAmountMinus = "minus_button";

    private const string ussBuyLayer = "buy_layer_container";
    private const string ussBuyButton = "buy_button";

    private const string ussInfoWindow = "info_window";
    private const string ussInfoLayerTop = "info_layer_top";
    private const string ussInfoLayerBottom = "info_layer_bottom";
    private const string ussHeader = "info_header_container";
    private const string ussHeaderText = "info_header_text";

    private const string ussSupplyScroll = "supply_scroll";

    private const string ussTopTextContainer = "top_text_container";

    private const string ussBottomContainer = "bottom_popup_container";
    private const string ussInsider1 = "insider_1";
    private const string ussInsider2 = "insider_2";
    private const string ussInsider3 = "insider_3";
    private const string ussMiniLayer = "inside_mini_layers";
    private const string ussMiniLayerLabel = "mini_layer_label";

    private const string ussMarginBottom = "margin_bottom";

    private const string ussTradeLayer = "trade_layer";
    private const string ussLeftArrow = "left_arrow";
    private const string ussRightArrow = "right_arrow";
















    public Label amount = new Label();
    public Button buyButton = new Button();
    public Button rightArrow = new Button();
    public Button leftArrow = new Button();
    public Label itemName = new Label();
    public Label supplyNumber= new Label();
    public Label demandNumber = new Label();
    public Label inInventoryNumber = new Label();
    public Label inShopNumber = new Label();



    int amount_;


    public Buy_Sell()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(stylesheet));
        AddToClassList(ussContainer);
        
        //top item name container
        VisualElement topText= new VisualElement();
        topText.name = "TopTextContainer";
        hierarchy.Add(topText);
        topText.AddToClassList(ussTopTextContainer);

        //item name
        itemName.name = "Item_Name";
        itemName.text = "Item";
        topText.Add(itemName);
        //itemName.AddToClassList(ussItemNameText);



        //info window
        VisualElement infoWindow= new VisualElement();
        infoWindow.name = "info_Window";
        hierarchy.Add(infoWindow);
        infoWindow.AddToClassList(ussInfoWindow);

        //info layer top
        VisualElement infoTop= new VisualElement();
        infoTop.name = "info_top";
        infoWindow.Add(infoTop);
        infoTop.AddToClassList(ussInfoLayerTop);
        //info layer top header
        VisualElement headerLayer = new VisualElement();
        headerLayer.name = "top_layer_header";
        infoTop.Add(headerLayer);
        headerLayer.AddToClassList(ussHeader);
        //info layer top header text
        Label topHeader = new Label();
        topHeader.name = "topHeader";
        topHeader.text = "Supply";
        topHeader.AddToClassList(ussHeaderText);
        headerLayer.Add(topHeader);
        //supply scroll view
        ScrollView supplyItems= new ScrollView();
        supplyItems.name = "supply_Items";
        supplyItems.AddToClassList(ussSupplyScroll);
        infoTop.Add(supplyItems);

        for(int i=0; i<10; i++)
        {
            infoItem test = new infoItem();
            test.name = "SupplyItem" + i;
            supplyItems.Add(test);
        }


    
        

        //info layer bottom
        VisualElement infoBottom = new VisualElement();
        infoBottom.name = "info_bottom";
        infoWindow.Add(infoBottom);
        infoBottom.AddToClassList(ussInfoLayerBottom);
        //info layer bottom header
        VisualElement headerBottomLayer = new VisualElement();
        headerBottomLayer.name = "top_layer_header";
        infoBottom.Add(headerBottomLayer);
        headerBottomLayer.AddToClassList(ussHeader);
        //info layer bottom header text
        Label bottomHeader = new Label();
        bottomHeader.name = "topHeader";
        bottomHeader.text = "Demand";
        bottomHeader.AddToClassList(ussHeaderText);
        headerBottomLayer.Add(bottomHeader);
        
        //demand scroll view
        ScrollView demandItems = new ScrollView();
        demandItems.name = "demand_Items";
        demandItems.AddToClassList(ussSupplyScroll);
        infoBottom.Add(demandItems);

        for (int i = 0; i < 10; i++)
        {
            infoItem test = new infoItem();
            test.name = "DemandItem" + i;
            demandItems.Add(test);
        }


        //popup window
        VisualElement window = new VisualElement();
        window.name = "PopUp";
        hierarchy.Add(window);
        window.AddToClassList(ussPopUpWindow);
        
        //firsthorizontal element
        VisualElement transactionLayer= new VisualElement();
        transactionLayer.name = "Transaction_Layer";
        window.Add(transactionLayer);
        transactionLayer.AddToClassList(ussHorizontalContainer);
        
        //transactiontext
        Label transactionText = new Label();
        transactionText.name = "Transaction_Text";
        transactionText.text = "Transaction";
        transactionLayer.Add(transactionText);
        transactionText.AddToClassList(ussTransactionText);

        //close button
        Button closeButton= new Button();
        closeButton.name= "Cancel_Button";
        transactionLayer.Add(closeButton);
        closeButton.AddToClassList(ussCancelButton);

        closeButton.clicked += closeTransactionWindow;


        //second horizontal element
        //VisualElement itemNameLayer= new VisualElement();
        //itemNameLayer.name = "Item_Name_Layer";       
        //window.Add(itemNameLayer);
        //itemNameLayer.AddToClassList(ussItemNameContainer);


        //second horizontal element
        VisualElement popupBottom = new VisualElement();
        popupBottom.name = "Bottom_Popup";
        window.Add(popupBottom);
        popupBottom.AddToClassList(ussBottomContainer);

        //insider 1
        VisualElement insider1  = new VisualElement();
        insider1.name = "Insider_1";
        popupBottom.Add(insider1);
        insider1.AddToClassList(ussInsider1);

        //insider 1 mini layer 1
        Label supply = new Label();
        supply.name = "Supply";
        supply.text = "Supply";
        insider1.Add(supply);
        supply.AddToClassList(ussMiniLayerLabel);
        //insider 1 mini layer 2
        supplyNumber.name = "Supply_Number";
        supplyNumber.text = "3";
        insider1.Add(supplyNumber);
        supplyNumber.AddToClassList(ussMiniLayerLabel);
        supplyNumber.AddToClassList(ussMarginBottom);

        //insider 2 mini layer 1
        Label demand = new Label();
        demand.name = "Demand";
        demand.text = "Demand";
        insider1.Add(demand);
        demand.AddToClassList(ussMiniLayerLabel);
        //insider 2 mini layer 2
        demandNumber.name = "Demand_Number";
        demandNumber.text = "3";
        insider1.Add(demandNumber);
        demandNumber.AddToClassList(ussMiniLayerLabel);

        //insider 2
        VisualElement insider2 = new VisualElement();
        insider2.name = "Insider_2";
        popupBottom.Add(insider2);
        insider2.AddToClassList(ussInsider2);


        //insider 2 left and right arrows for trade

        VisualElement tradeLayer = new VisualElement();
        tradeLayer.name = "Trade_Layer";
        insider2.Add(tradeLayer);
        tradeLayer.AddToClassList(ussTradeLayer);

        leftArrow.name= "Left_Arrow";
        tradeLayer.Add(leftArrow);
        leftArrow.AddToClassList(ussLeftArrow);

        leftArrow.clicked += Leftarrow;

        rightArrow.name = "Right_Arrow";
        tradeLayer.Add(rightArrow);
        rightArrow.AddToClassList(ussRightArrow);

        rightArrow.clicked += Rightarrow;


        //insider 2 plus minus
        VisualElement amountLayer = new VisualElement();
        amountLayer.name ="Amount_Layer";
        insider2.Add(amountLayer);
        //button -
        Button minusButton = new Button();
        minusButton.name = "Plus_Button";
        amountLayer.Add(minusButton);
        minusButton.AddToClassList(ussChangeAmountMinus);

        minusButton.clicked += DecreaseAmount;
        //amount
        amount.name = "Amount";
        amount.text = amount_.ToString();
        amountLayer.Add(amount);
        amount.AddToClassList(ussAmount);

        amountLayer.AddToClassList(ussAmountLayerContainer);
        //button +
        Button plusButton = new Button();
        plusButton.name = "Plus_Button";
        amountLayer.Add(plusButton);
        plusButton.AddToClassList(ussChangeAmountPlus);

        plusButton.clicked += IncreaseAmount;

        //insider 2 mini layer 1
        VisualElement I2miniLayer1 = new VisualElement();
        I2miniLayer1.name = "I2Mini_Layer_1";
        insider2.Add(I2miniLayer1);
        I2miniLayer1.AddToClassList(ussMiniLayer);

        Label salePriceText= new Label();
        salePriceText.name = "Sale_Price_Text";
        salePriceText.text = "Sale Price: ";
        I2miniLayer1.Add(salePriceText);
        salePriceText.AddToClassList(ussMiniLayerLabel);
        

        Label salePrice = new Label();
        salePrice.name = "Sale_Price";
        salePrice.text = "10";
        I2miniLayer1.Add(salePrice);
        salePrice.AddToClassList(ussMiniLayerLabel);

        //insider 2 mini layer 2
        VisualElement miniLayer2 = new VisualElement();
        miniLayer2.name = "I2Mini_Layer_2";
        insider2.Add(miniLayer2);
        miniLayer2.AddToClassList(ussMiniLayer);

        Label buyPriceText = new Label();
        buyPriceText.name = "Buy_Price";
        buyPriceText.text = "Buy Price: ";
        miniLayer2.Add(buyPriceText);
        buyPriceText.AddToClassList(ussMiniLayerLabel);

        Label buyPrice = new Label();
        buyPrice.name = "Buy_Price";
        buyPrice.text = "20";
        miniLayer2.Add(buyPrice);
        buyPrice.AddToClassList(ussMiniLayerLabel);

        //insider 2 mini layer 3
        VisualElement miniLayer3 = new VisualElement();
        miniLayer3.name = "I2Mini_Layer_3";
        insider2.Add(miniLayer3);
        miniLayer3.AddToClassList(ussMiniLayer);

        Label totalPriceText = new Label();
        totalPriceText.name = "Total_Price";
        totalPriceText.text = "Total Price: ";
        miniLayer3.Add(totalPriceText);
        totalPriceText.AddToClassList(ussMiniLayerLabel);

        Label totalPrice = new Label();
        totalPrice.name = "Total_Price";
        totalPrice.text = "30";
        miniLayer3.Add(totalPrice);
        totalPrice.AddToClassList(ussMiniLayerLabel);

        

        ////insider 2 buybutton
        //VisualElement buyLayer = new VisualElement();
        //buyLayer.name ="Buy_Layer";
        //insider2.Add(buyLayer);
        //buyLayer.AddToClassList(ussBuyLayer);
        
        ////buy button
        //buyButton.name = "Transaction_Button";
        //buyLayer.Add(buyButton);
        //buyButton.text = "Buy";
        //buyButton.AddToClassList(ussBuyButton);
        
        
        //buyButton.clicked += Sell;

        //insider 3
        VisualElement insider3 = new VisualElement();
        insider3.name = "Insider_3";
        popupBottom.Add(insider3);
        insider3.AddToClassList(ussInsider3);

        //insider 3 mini layer 1
        VisualElement I3minilayer1= new VisualElement();
        I3minilayer1.name = "I3Mini_Layer_1";
        insider3.Add(I3minilayer1);
        //I3minilayer1.AddToClassList(ussMiniLayer);
        I3minilayer1.AddToClassList(ussMarginBottom);


        Label inInventoryText = new Label();
        inInventoryText.name = "In_Inventory_Text";
        inInventoryText.text = "In Inventory: ";
        I3minilayer1.Add(inInventoryText);
        inInventoryText.AddToClassList(ussMiniLayerLabel);


        inInventoryNumber.name = "In_Inventory_Number";
        inInventoryNumber.text = "1";
        I3minilayer1.Add(inInventoryNumber);
        inInventoryNumber.AddToClassList(ussMiniLayerLabel);

        //insider 3 mini layer 2
        VisualElement I3minilayer2 = new VisualElement();
        I3minilayer2.name = "I3Mini_Layer_2";
        insider3.Add(I3minilayer2);
        I3minilayer2.AddToClassList(ussMarginBottom);

        Label inShopText = new Label();
        inShopText.name = "In_Shop_Text";
        inShopText.text = "In Shop: ";
        I3minilayer2.Add(inShopText);
        inShopText.AddToClassList(ussMiniLayerLabel);

        inShopNumber.name = "In_Shop_Number";
        inShopNumber.text = "1";
        I3minilayer2.Add(inShopNumber);
        inShopNumber.AddToClassList(ussMiniLayerLabel);

        //insider 3 mini layer 3
        VisualElement I3minilayer3 = new VisualElement();
        I3minilayer3.name = "I3Mini_Layer_3";
        insider3.Add(I3minilayer3);
        I3minilayer3.AddToClassList(ussMarginBottom);


        Label boughtForText = new Label();
        boughtForText.name = "Bought_For_Text";
        boughtForText.text = "Bought For: ";
        I3minilayer3.Add(boughtForText);
        boughtForText.AddToClassList(ussMiniLayerLabel);

        Label boughtForNumber = new Label();
        boughtForNumber.name = "Bought_For_Number";
        boughtForNumber.text = "10";
        I3minilayer3.Add(boughtForNumber);
        boughtForNumber.AddToClassList(ussMiniLayerLabel);

        //insider 3 mini layer 4
        VisualElement I3minilayer4 = new VisualElement();
        I3minilayer4.name = "I3Mini_Layer_4";
        insider3.Add(I3minilayer4);
        //I3minilayer3.AddToClassList(ussMiniLayer);
        I3minilayer4.AddToClassList(ussMarginBottom);


        Label emptySlotText = new Label();
        emptySlotText.name = "Empty_Slot_Text";
        emptySlotText.text = "Empty Slot: ";
        I3minilayer4.Add(emptySlotText);
        emptySlotText.AddToClassList(ussMiniLayerLabel);

        Label emptySlotNumber = new Label();
        emptySlotNumber.name = "Empty_Slot_Number";
        emptySlotNumber.text = "10";
        I3minilayer4.Add(emptySlotNumber);
        emptySlotNumber.AddToClassList(ussMiniLayerLabel);

    }

    public event Action increase;
    public event Action decrease;
    public event Action closeTransaction;
    public event Action sell;
    public event Action rightarrow;
    public event Action leftarrow;




    public void IncreaseAmount()
    {
        increase?.Invoke();
    }

    public void DecreaseAmount()
    {
        decrease?.Invoke();
    }
    public void closeTransactionWindow()
    {
        closeTransaction?.Invoke();
    }

    void Sell()
    {
        sell?.Invoke();
    }

    public void Rightarrow()
    {
        rightarrow?.Invoke();
    }

    public void Leftarrow()
    {
        leftarrow?.Invoke();
    }

}
