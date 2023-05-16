using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot : Button
{
    public new class UxmlFactory : UxmlFactory <InventorySlot> { }


    private const string stylesheetInv = "Inventory_Slot";

    private const string ussSlotContainerInv = "inventorySlotContainer";
    private const string ussSlotIconInv = "inventorySlotIcon";
    private const string ussSlotSelector = "slotSelector";
    private const string ussAmount = "amount";
    private const string ussAmountLabel = "amountLabel";
    private const string ali = "ali";



    public VisualElement amount = new VisualElement();
    public InventorySlot()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(stylesheetInv));
        AddToClassList(ussSlotSelector);

        VisualElement window = new VisualElement();
        window.name = "Inventory_Slot";
        window.AddToClassList(ussSlotContainerInv);
        hierarchy.Add(window);
        

        //Create a new Image element and add it to the root
        VisualElement Icon = new VisualElement();
        Icon.name = "Inventory_Icon";
        window.Add(Icon);
        Icon.AddToClassList(ussSlotIconInv);

        VisualElement amount = new VisualElement();
        amount.name = "Amount";
        Icon.Add(amount);
        amount.AddToClassList(ussAmount);

        Label amountLabel = new Label();
        amountLabel.name = "Inventory_Amount";
        amount.Add(amountLabel);

        Label supplyInfo = new Label();
        supplyInfo.name = "Supply_Info";
        amount.Add(supplyInfo);
        supplyInfo.style.display = DisplayStyle.None;

        Label demandInfo = new Label();
        demandInfo.name = "Demand_Info";
        amount.Add(demandInfo);
        demandInfo.style.display = DisplayStyle.None;



        amountLabel.AddToClassList(ussAmountLabel);
        amountLabel.AddToClassList(ali);

 


    }


}
