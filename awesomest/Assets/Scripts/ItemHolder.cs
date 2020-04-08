using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder
{
   public Dictionary<Item, int> itemPrices;


   public ItemHolder()
	{
        itemPrices = new Dictionary<Item, int>();
	}
   public Dictionary<Item, int> getItems()
	{
        return this.itemPrices;
	}

    public Dictionary<Item, int> generateItemPrices()
	{
        itemPrices.Add(new Item(1,"building", 100, "this is a building"), 100);
        itemPrices.Add(new Item(2,"sidewalk", 20, "this is a sidewalk"),20);
        itemPrices.Add(new Item(3, "bomb", 200, "this is a bomb"),200);
        return itemPrices;
	}

    public Item itemConverter(string itemInfo)
	{
        string itemName = itemInfo.Split(' ')[0];
        Debug.Log(itemName);
        foreach(Item item in itemPrices.Keys)
		{
			if (item.getName() == itemName)
			{
                return item;
			}
		}
        return null;
	}

    public Item itemConverter_inven(string itemInfo)
    {
        string itemName = itemInfo;
        Debug.Log(itemName);
        foreach (Item item in itemPrices.Keys)
        {
            if (item.getName() == itemName)
            {
                return item;
            }
        }
        return null;
    }

}

