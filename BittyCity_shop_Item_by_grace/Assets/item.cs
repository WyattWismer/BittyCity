using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    private itemSprite sprite;
    private itemInfo info;

    public Item(string name, int price, string description)
	{
        this.info = new itemInfo(name, price, description);
        this.sprite = new itemSprite();
	}

    public string getName()
	{
        return this.info.getName();
	}

    public string getDescription()
	{
        return this.info.getDescription();
	}

    public void display(int x, int y)
	{
        this.sprite.display(x, y);
	}

    public int getPrice()
	{
        return this.info.getPrice();
	}
   
}

public class itemSprite
{
    public itemSprite()
    {

    }

    public void display(int x, int y)
    {

    }
}

public class itemInfo
{
    private string name;
    private int price;
    private string description;

    public itemInfo(string name, int price, string description)
    {
        this.name = name;
        this.price = price;
        this.description = description;
    }

    public string getName()
    {
        return this.name;
    }

    public int getPrice()
	{
        return this.price;
	}

    public string getDescription()
    {
        return this.description;
    }

   

}

