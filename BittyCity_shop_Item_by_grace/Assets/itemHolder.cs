using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder
{
   protected Dictionary<Item, price> items;

   public ItemHolder()
	{
        items = new Dictionary<Item, price>();
	}

	public void updateShopItem(string name, int price, string description)
	{
        Item item = new Item(name, price, description);
        this.items.Add(item, item.getPrice);
	}


   public Dictionary<Item, price> getItems()
	{
        return this.items;
	}

    
}

