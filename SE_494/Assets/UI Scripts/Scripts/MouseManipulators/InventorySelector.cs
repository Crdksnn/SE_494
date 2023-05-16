using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySelector : MouseManipulator
{
    public Controller controller;
    
    Label amount;

    public InventorySelector(VisualElement root)
    {
        amount = root.Q<Label>(className:"item_name_text");
    }
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);


    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);


    }

    protected void OnMouseDown(MouseDownEvent e)
    {
        //if (amount.text != target.userData.ToString())
        //    amount.text = target.userData.ToString();

        Setup.dummyPath = target.Q("Inventory_Icon").userData.ToString();
        Setup.dummyAmount = target.Q<Label>("Inventory_Amount").userData.ToString();

        Setup.dummyPlace = target.parent.name;
        Setup.dummyDataInv = target.userData.ToString();
        Setup.invPrice = target.Q("Amount").userData.ToString();


    }



}
