using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class infoItem : VisualElement
{

    public new class UxmlFactory : UxmlFactory <infoItem> { }

    public static string ussMain = "info";
    public static string ussContainer = "container";
    public static string ussLabel = "labels";
    public static string ussSelector = "info_selector";


    public infoItem()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(ussMain));
        AddToClassList(ussContainer);
        AddToClassList(ussSelector);

        VisualElement infoBar = new VisualElement();
        infoBar.AddToClassList(ussContainer);
        hierarchy.Add(infoBar);

        Label city = new Label();
        infoBar.Add(city);
        city.name = "City";
        city.text = "Ankara";
        city.AddToClassList(ussLabel);

        Label demand = new Label();
        infoBar.Add(demand);
        demand.name = "Supply";
        demand.text = "2";
        demand.AddToClassList(ussLabel);

        Label price = new Label();
        infoBar.Add(price);
        price.name = "Price";
        price.text = "12";
        price.AddToClassList(ussLabel);

        Label distance = new Label();
        infoBar.Add(distance);
        distance.name = "Distance";
        distance.text = "1";
        distance.AddToClassList(ussLabel);

        Label date = new Label();
        infoBar.Add(date);
        date.name = "Date";
        date.text = "3 days ago";
        date.AddToClassList(ussLabel);




    }

}
