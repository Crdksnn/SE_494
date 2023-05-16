using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Game game;
    public UI ui;

    private List<Item> items= new List<Item>();

    //Shop_Window shop=new Shop_Window();
    //Drag_Area drag=new Drag_Area();




    private void Start()
    {

    }

    public void UpdateUI()
    {
        
    }

    public List<Item> GetListFromGame()
    {
        return game.Items;
    }




}
