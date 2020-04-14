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
	public Button toggleShop;


    public void displayItems()
	{
		//SceneManager.LoadScene("Shop");
	}

    public void hideScreen()
    {
		//SceneManager.LoadScene("SampleScene");
    }

	public static void displayWallet(int amount)
	{
		userWallet = GameObject.Find("wallet").GetComponent<Text>();
		userWallet.text = "Wallet: $" + amount.ToString();
	}

	public static void displayInsufficentFunds()
    {
		GameObject am = GameObject.Find("AchievementManager");
		am.GetComponent<AchievementControl>().NotificationUpdate("Insufficient Funds", "Cannot complete purchase at this time");
		//SceneManager.LoadScene("alert");
	}
  
	public void GameOver()
    {
		this.gameObject.SetActive(false);
	}

	public void Start()
	{
		toggleShop.onClick.AddListener(delegate
		{
			//this.gameObject.SetActive(false);
			this.gameObject.SetActive(!this.gameObject.activeSelf);
			if (this.gameObject.activeSelf)
            {
				ShopScreen.displayWallet(ShopControl.shopConents.getCurrency());
			}
		});

		displayWallet(ShopControl.getCurrency());

		this.gameObject.SetActive(false);
	}

    public void Update()
	{
	}

}
