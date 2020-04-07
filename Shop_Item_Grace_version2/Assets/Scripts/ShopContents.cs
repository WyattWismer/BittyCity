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

    public void changeCurrency(Item item)
	{
        this.userCurrency -= itemPrices[item];
	}

    public int getCurrency()
	{
        return this.userCurrency;
	}

    public int checkPurchase(Item item)
	{
        return userCurrency - itemPrices[item];
	}
}
