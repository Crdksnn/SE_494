using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopSelector : MouseManipulator
{
    public Controller controller;

    List<supplyItems> list=new List<supplyItems>();
    Label amount;

    public ShopSelector(VisualElement root)
    {
        amount = root.Q<Label>(className: "item_name_text");
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

        Setup.dummyPath = target.Q("Shop_Icon").userData.ToString();
        Setup.dummyAmount = target.Q<Label>("Shop_Amount").userData.ToString();
        Setup.dummyPlace = target.parent.name;
        Setup.dummyDataShop = target.userData.ToString();
        Setup.shopPrice = target.Q("Amount").userData.ToString();

        list.AddRange(Resources.LoadAll<supplyItems>("supply/" + Setup.dummyDataShop + "info"));
        Setup.dummyList = list;

    }


}
