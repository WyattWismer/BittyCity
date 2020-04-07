using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopScreen : ItemDisplayer
{

	public GameObject itemSelected;
	public Transform shop;
	public Text itemText;

	public static void displayItems(Dictionary<Item, int> itemPrices)
	{

	}

	public void acceptItemPurchase()
	{
		itemSelected = EventSystem.current.currentSelectedGameObject;
		Debug.Log(itemSelected);
	}
    

	public void Start()
	{
		shop = transform.Find("Shop");
		
	}

    public void Update()
	{
		acceptItemPurchase();
	}
}
