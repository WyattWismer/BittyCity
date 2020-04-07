using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
	public static ShopScreen shopScreen = new ShopScreen();
    public static ShopConents shopConents = new ShopConents(itemHolder.generateItemPrices(), 100);
    public static ItemHolder itemHolder = new ItemHolder();

    public static Dictionary<Item, int> itemPrices = shopConents.getItemPrices(); 

    
    public static void loadShopScreen()
	{
        ShopScreen.displayItems(itemPrices);
    }

   

    public void purchaseItem()
	{
        Item item = shopScreen.getSelectedItem();
        if (checkPurchase(item)){
            shopScreen.acceptItemPurchase();
            //Update usercurrency
			shopConents.changeCurrency(item);
            ItemControl.transferToInventory(item);
        }
        return;
    }

    public bool checkPurchase(Item item) 
	{
		//check if user has sufficient money
		if (shopConents.checkPurchase(item) >= 0)
		{
            return true;
		}
        return false;
	}
	// Start is called before the first frame update
	void Awake()
    {
	}

    // Update is called once per frame
    void Update()
    {

    }
}
