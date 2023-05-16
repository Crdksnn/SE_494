using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour {

    public List<Item> Items = new List<Item>();

    public int bag = 5;
    public void GetList()
    {
         Items.AddRange(Resources.LoadAll<Item>("Items"));
        //DataManager.myInventory.addItem
    }

    
















}
