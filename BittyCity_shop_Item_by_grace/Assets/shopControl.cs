using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    private ShopScreen shopScreen;
    private ShopConents shopConents;
   

    public ShopControl()
	{
        ItemHolder itemHolder = new ItemHolder();
        shopConents = new ShopConents(itemHolder.generateItemPrices(), 100);
        shopScreen = new ShopScreen();
	}
    public static void loadShopScreen()
	{

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
        Debug.Log(shopConents.getItemPrices().Count);
	}

    // Update is called once per frame
    void Update()
    {

    }
}
