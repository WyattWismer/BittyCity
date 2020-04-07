using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;


public class ShopScreen : ItemDisplayer
{

	public GameObject itemSelected;

	public void displayItems(Dictionary<Item, int> itemPrices)
	{
		foreach (Item item in itemPrices.Keys)
		{

		}
	}

	public void acceptItemPurchase()
	{

		deselectItem();
	}

	

	public void Update()
	{
		if (Selection.Contains(itemSelected))
			Debug.Log("I'm selected!");
	}

}
