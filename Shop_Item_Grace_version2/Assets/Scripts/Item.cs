using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemInfo info;

    public Item(string name, int price, string description)
	{
        this.info = new ItemInfo(name, price, description);
	}

   
}

public class ItemInfo
{
    private string name;
    private int price;
    private string description;

    public ItemInfo(string name, int price, string description){
        this.name = name;
        this.description = description;
        this.price = price;
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

public class ItemSprite
{
    public ItemSprite()
    {

    }

    public void display(int x, int y)
    {

    }
}

