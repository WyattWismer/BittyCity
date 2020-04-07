using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder
{
   protected List<Item> items;


   public ItemHolder()
	{

	}
   public List<Item> getItems()
	{
        return this.items;
	}

    public Dictionary<Item, int> generateItemPrices()
	{
        Dictionary<Item, int> itemPrices = new Dictionary<Item, int>();
        itemPrices.Add(new Item(1,"building", 100, "this is a building"), 100);
        itemPrices.Add(new Item(2,"sidewalk", 20, "this is a sidewalk"),20);
        itemPrices.Add(new Item(3, "bomb", 200, "this is a bomb"),200);
        return itemPrices;
	}
}

