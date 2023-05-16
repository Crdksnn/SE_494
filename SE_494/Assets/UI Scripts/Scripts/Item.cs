using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string item_Name;
    public string item_Type;
    public string img_path;
    public int item_Price;
    public int itemAmount;
    public int demand;
    public int supply;

}
