using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    private ShopScreen shopScreen;
    private ShopConents shopConents;
    public GameObject panel;
    private MeshRenderer myRenderer;


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
        shopScreen.acceptItemPurchase();
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
	void Start()
    {
        Debug.Log(shopConents.getItemPrices().Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
