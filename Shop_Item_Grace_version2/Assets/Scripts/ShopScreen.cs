using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopScreen : ItemDisplayer
{

	public GameObject itemSelected;
	public GameObject shopVisiblity;
	public Transform shop;
	public int itemNum;

	public static void displayItems(Dictionary<Item, int> itemPrices)
	{
		
	}

	public void acceptItemPurchase()
	{
		deselectItem();
	}


	public void OnDeselect()
	{
		Debug.Log("deselected");
	}
	public void Start()
	{
		shop = transform.Find("Shop");
		itemSelected = shop.Find("item").gameObject;
	
	}

    public void Update()
	{
        
	}
}
