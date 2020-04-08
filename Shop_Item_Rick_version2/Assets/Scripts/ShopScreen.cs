using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ShopScreen : ItemDisplayer
{
    //it will be attached to panel
	public static Transform panel;
	public static Text userWallet;

    public static void displayItems()
	{
		SceneManager.LoadScene("SampleScene");
	}
	public static void displayWallet(int amount)
	{
		userWallet = GameObject.Find("wallet").GetComponent<Text>();
		userWallet.text = "Wallet: $" + amount.ToString();
	}

	public static void displayInsufficentFunds()
    {
		SceneManager.LoadScene("alert");
    }
  
	public void Start()
	{
		displayWallet(ShopControl.getCurrency());
	}

    public void Update()
	{
	}

}
