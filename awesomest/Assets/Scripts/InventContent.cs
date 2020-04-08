using System.Collections.Generic;
using System;

public class InventContent
{
	private static Dictionary<int, int> inventory;

    public InventContent(){
		inventory = new Dictionary<int, int>();
		inventory.Add(1, 0);
		inventory.Add(2, 0);
		inventory.Add(3, 0);
	}

	public int getItemAmounts(Item i)
	{
		return inventory[i.getItemID()];
	}

	public void addItem(Item i)
	{
		inventory[i.getItemID()] += 1;
	}

	public void removeItem(Item i)
	{
        if (inventory[i.getItemID()] >= 1)
        {
			inventory[i.getItemID()] -= 1;
		}
	}

    public Dictionary<int, int> getInventoryDict()
    {
		return inventory;
    }
}
