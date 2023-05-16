using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drag_Area : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Drag_Area> { }

    private const string stylesheet = "Drag_Area";


    private const string ussDragArea = "dragArea";

    public Drag_Area()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(stylesheet));
        AddToClassList(ussDragArea);
    }

}
