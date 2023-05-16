using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Dragger : MouseManipulator
{
    Controller controller;

    private Vector2 startPos;
    private Vector2 itemStartPosGlobal;
    private Vector2 itemStartPosLocal;

    VisualElement dragArea;
    VisualElement itemContainer;

    List<VisualElement> dropZone;
    List<VisualElement> dropZoneShop;


    bool isActive;

    public Item_Dragger(VisualElement root)
    {
        dragArea = root.Query(className:"dragArea");
        dropZone = root.Query(className: "inventorySlotContainer").ToList();
        dropZoneShop = root.Query(className: "shopSlotIcon").ToList();


        isActive = false;
    }


    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent e)
    {
        // get reference to the icon container for later
        itemContainer = target.parent;

        // get mouse start pos
        startPos = e.localMousePosition;

        // get both target start pos
        //itemStartPosGlobal = new Vector2(target.worldBound.xMin, target.worldBound.yMin);
       itemStartPosGlobal = target.worldBound.position;
        itemStartPosLocal = target.layout.position;

        // enable dragArea
        dragArea.style.display = DisplayStyle.Flex;
        dragArea.Add(target);

        // correct pos after repositioning
        target.style.top = itemStartPosGlobal.y;
        target.style.left = itemStartPosGlobal.x;

        
        isActive = true;
        target.CaptureMouse();
        e.StopPropagation();
    }

    protected void OnMouseMove(MouseMoveEvent e)
    {
        if (!isActive || !target.HasMouseCapture())
            return;

        Vector2 diff = e.localMousePosition - startPos;

        target.style.top = target.layout.y + diff.y;
        target.style.left = target.layout.x + diff.x;

        e.StopPropagation();
    }

    protected void OnMouseUp(MouseUpEvent e)
    {
        if (!isActive || !target.HasMouseCapture())
            return;


        for (int i = 0; i < dropZone.Count; i++)
        {

            if (target.worldBound.Overlaps(dropZone[i].worldBound))
            {
                foreach (VisualElement ve in dropZone[i].Children())
                {
                    ve.style.backgroundImage = target.style.backgroundImage;
                    target.style.backgroundImage = null;

                    target.style.top = dropZone[i].contentRect.center.y - target.layout.height / 2;
                    target.style.left = dropZone[i].contentRect.center.x - target.layout.width / 2;
                }


            }


            else if (i == dropZone.Count - 1)
            {
                itemContainer.Add(target);

                target.style.top = itemStartPosLocal.y - itemContainer.contentRect.position.y;
                target.style.left = itemStartPosLocal.x - itemContainer.contentRect.position.x;
            }
        }


        isActive = false;
        target.ReleaseMouse();
        e.StopPropagation();

        dragArea.style.display = DisplayStyle.None;
    }


}




