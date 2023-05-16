using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Notebook_Frame : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Notebook_Frame> { }

    private const string ussMain = "NStyle";

    private const string ussWindow = "window_container";
    private const string ussNotebook = "notebook";
    private const string ussTopContainer = "top_container";
    private const string ussTopLabel = "top_label";
    private const string ussBottomContainer = "bottom_container";




    public Notebook_Frame() 
    {
        styleSheets.Add(Resources.Load<StyleSheet>(ussMain));
        AddToClassList(ussWindow);


        //notebook
        VisualElement notebook = new VisualElement();
        hierarchy.Add(notebook);
        notebook.AddToClassList(ussNotebook);
        
        //top container
        VisualElement topContainer = new VisualElement();
        notebook.Add(topContainer);
        topContainer.AddToClassList(ussTopContainer);

        Label itemNameLabel= new Label();
        topContainer.Add(itemNameLabel);
        itemNameLabel.text = "Item";
        itemNameLabel.AddToClassList(ussTopLabel);

        Label locationLabel = new Label();
        topContainer.Add(locationLabel);
        locationLabel.text = "Location";
        locationLabel.AddToClassList(ussTopLabel);

        Label itemPriceLabel = new Label();
        topContainer.Add(itemPriceLabel);
        itemPriceLabel.text = "Price";
        itemPriceLabel.AddToClassList(ussTopLabel);

        Label itemAmountLabel= new Label();
        topContainer.Add(itemAmountLabel);
        itemAmountLabel.text = "Amount";
        itemAmountLabel.AddToClassList(ussTopLabel);

        Label itemShop = new Label();
        topContainer.Add(itemShop);
        itemShop.text = "Shop";
        itemShop.AddToClassList(ussTopLabel);
        //top container

        //bottom container
        VisualElement bottomContainer= new VisualElement();
        notebook.Add(bottomContainer);
        bottomContainer.AddToClassList(ussBottomContainer);

        ScrollView itemList= new ScrollView();
        bottomContainer.Add(itemList);
        for(int i = 0; i < 20; i++)
        {
            Notebook_Slots slot = new Notebook_Slots();
            itemList.Add(slot);
        }
    }

}
