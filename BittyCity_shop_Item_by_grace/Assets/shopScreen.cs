using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShopScreen : ItemDisplayer {

    

	public void displayItems(Dictionary<Item, int> itemPrices)
	{
       foreach(Item item in itemPrices.Keys)
		{

		}
	}

    public void acceptItemPurchase()
	{
		deselectItem();
	}

}
