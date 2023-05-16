using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopSlot : Button
{
    public new class UxmlFactory : UxmlFactory<ShopSlot> { }


    private const string stylesheetInv = "Inventory_Slot";

    private const string ussShopContainerInv = "shopSlotContainer";
    private const string ussSlotIconShop = "shopSlotIcon";
    private const string ussSlotSelector = "slotSelector";
    private const string ussAmount = "amount";
    private const string ussAmountLabel = "amountLabel";





    public ShopSlot()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(stylesheetInv));
        AddToClassList(ussSlotSelector);

        VisualElement window = new VisualElement();
        window.name = "Shop_Slot";
        window.AddToClassList(ussShopContainerInv);
        hierarchy.Add(window);
        //Create a new Image element and add it to the root
        VisualElement Icon = new VisualElement();
        Icon.name = "Shop_Icon";
        window.Add(Icon);
        Icon.AddToClassList(ussSlotIconShop);


  

        VisualElement amount = new VisualElement();
        amount.name = "Amount";
        Icon.Add(amount);
        amount.AddToClassList(ussAmount);

        Label amountLabel = new Label();
        amountLabel.name= "Shop_Amount";
        amount.Add(amountLabel);

        Label supplyInfo = new Label();
        supplyInfo.name = "Supply_Info";
        amount.Add(supplyInfo);
        supplyInfo.style.display = DisplayStyle.None;

        Label demandInfo = new Label();
        demandInfo.name = "Demand_Info";
        amount.Add(demandInfo);
        demandInfo.style.display = DisplayStyle.None;





        amount.AddToClassList(ussAmountLabel);



    }


}
