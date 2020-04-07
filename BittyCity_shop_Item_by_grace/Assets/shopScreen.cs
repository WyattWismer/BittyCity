using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShopScreen : ItemDisplayer {

	public GameObject itemInfoText;

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

    public void onTrigger()
	{
		Debug.log("Hit");
	}

    public void Start()
	{

	}

}
