using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    protected string name;
    protected string description;
    protected int price;
    protected int itemID;

    public Item(int itemID, string name, int price, string description)
    {
        this.name = name;
        this.price = price;
        this.itemID = itemID;
        this.description = description;
    }

    public int getItemID()
    {
        return this.itemID;
    }

    public string getName()
    {
        return this.name;

    }

    public string getDescription()
    {
        return this.description;
    }

    public int getPrice()
    {
        return this.price;
    }

}