using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
	public static ShopScreen shopScreen = new ShopScreen();
    public static ItemHolder itemHolder = new ItemHolder();
    public static ShopConents shopConents = new ShopConents(itemHolder.generateItemPrices(), 100);

    public static Dictionary<Item, int> itemPrices = shopConents.getItemPrices(); 

    
    public static void loadShopScreen()
	{
      
        ShopScreen.displayItems();
    }

    public void purchaseItem()
	{
        string itemInfo = ShopScreen.selectedItem;
        Debug.Log(itemInfo);
        if (itemInfo == null) return;
        Item item = itemHolder.itemConverter(itemInfo);
        if (checkPurchase(item)){
            //Update usercurrency
			shopConents.changeCurrency(item);
            ShopScreen.displayWallet(shopConents.getCurrency());
            Debug.Log("Success");
			ItemControl.transferToInventory(item);
		}
        else
		{
            ShopScreen.displayInsufficentFunds();
            return;
        }
    }

    private bool checkPurchase(Item item) 
	{
		//check if user has sufficient money
		if (shopConents.checkPurchase(item) >= 0)
		{
            return true;
		}
        return false;
	}
    public static int getCurrency()
	{
        return shopConents.getCurrency();
	}

	
}
