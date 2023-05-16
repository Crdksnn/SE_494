using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Notebook_Slots : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Notebook_Slots> { }

    private const string ussContainer = "NotebookItems";
    private const string ussSlotContainer = "slot_container";
    private const string ussSlotLabel = "slot_label";

    public Notebook_Slots()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(ussContainer));
        AddToClassList(ussSlotContainer);

        VisualElement itemBar = new VisualElement();
        itemBar.AddToClassList(ussSlotContainer);
        hierarchy.Add(itemBar);

        Label slotName= new Label();
        itemBar.Add(slotName);
        slotName.text = "Wood";
        slotName.AddToClassList(ussSlotLabel);

        Label slotLocation = new Label();
        itemBar.Add(slotLocation);
        slotLocation.text = "Ankara";
        slotLocation.AddToClassList(ussSlotLabel);

        Label slotPrice = new Label();
        itemBar.Add(slotPrice);
        slotPrice.text = "20";
        slotPrice.AddToClassList(ussSlotLabel);

        Label slotAmount = new Label();
        itemBar.Add(slotAmount);
        slotAmount.text = "5";
        slotAmount.AddToClassList(ussSlotLabel);

        Label slotShop = new Label();
        itemBar.Add(slotShop);
        slotShop.text = "Market A";
        slotShop.AddToClassList(ussSlotLabel);
    }

}
