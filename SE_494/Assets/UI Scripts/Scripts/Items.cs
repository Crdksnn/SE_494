using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Items 
{
    public static List<string> strings= new List<string>();

    public static string gold = "allItems/gold";
    public static string iron = "allItems/iron";
    public static string shield = "allItems/shield";
    public static string statue = "allItems/statue";
    public static string sword = "allItems/sword";
    public static string wood = "allItems/wood";
    public static string armor= "allItems/armor";
    public static string knife = "allItems/knife";
    public static string shovel = "allItems/shovel";
    public static string spear = "allItems/spear";







    public static void additems()
    {
        strings.Add(gold);
        strings.Add(iron);
        strings.Add(shield);
        strings.Add(statue);
        strings.Add(sword);
        strings.Add(wood);
        strings.Add(armor);
        strings.Add(knife);
        strings.Add(shovel);
        strings.Add(spear);

    }



}
