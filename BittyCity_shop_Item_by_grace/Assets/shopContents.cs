using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopConents : ItemHolder
{
    private Dictionary<Item, int> itemPrices;
    private int userCurrency;

    public ShopConents(Dictionary<Item, int> itemPrices, int userCurrency)
	{
        this.itemPrices = itemPrices;
        this.userCurrency = userCurrency;
	}

    public Dictionary<Item, int> getItemPrices()
	{
        return this.itemPrices;
	}

    public void changeCurrency(int amount)
	{
        this.userCurrency = amount;
	}

    public int getCurrency()
	{
        return this.userCurrency;
	}

    public int checkPurchase(Item item)
	{
        return userCurrency - itemPrices[item];
	}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
