using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public static InventControl inventCtrl = new InventControl();

    public ItemControl()
	{

	}

    public static void transferToInventory(Item item)
	{
        inventCtrl.addItem(item);
	}

    public void useItem(Item item)
	{

	}

    public void loadShop()
	{
        ShopControl.loadShopScreen();
	}

    public void loadInventory()
	{
        inventCtrl.showInventory();

    }
   
        
}
