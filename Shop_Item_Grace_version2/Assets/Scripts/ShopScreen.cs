using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopScreen : ItemDisplayer
{
    //it will be attached to panel
	public static Transform panel;
	public static Text userWallet;

    public static void displayItems()
	{

	}
	public static void displayWallet(int amount)
	{
		userWallet = GameObject.Find("wallet").GetComponent<Text>();
		userWallet.text = "Wallet: $" + amount.ToString();
	}

	public void acceptItemPurchase()
	{
		
	}

  
	public void Start()
	{
	}

    public void Update()
	{
		acceptItemPurchase();
	}

}
