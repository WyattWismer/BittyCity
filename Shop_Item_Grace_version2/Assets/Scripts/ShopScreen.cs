using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopScreen : ItemDisplayer
{

	public Transform shop;
	public Text itemText;

	public static void displayItems()
	{

	}

	public void acceptItemPurchase()
	{
		
	}

  
	public void Start()
	{
		//shop = transform.Find("Shop");
		//itemSelected = shop.Find("building").gameObject;
		//GameObject obj = Instantiate(itemSelected, shop);
		//obj.layer = 5;
        
		
	}

    public void Update()
	{
		acceptItemPurchase();
	}

}
