using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private List<Item> items;


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
        itemPrices.Add(new Item("a", 1, "item a"), 1);
        itemPrices.Add(new Item("b", 2, "item b"), 2);
        return itemPrices;
	}
}

