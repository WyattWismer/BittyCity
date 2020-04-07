
public class InventControl
{
	private InventContent inventory;

	public void addItem(Item i)
	{
		inventory.addItem(i);
	}

	public void removeItem(Item i)
	{
		inventory.removeItem(i);
	}

	public void useItem(Item i)
	{
		string itemName = i.getName();
		switch (itemName)
        {
        	case "0":{

        	}
        	case "1":{

        	}
        }
		inventory.removeItem(i);
	}

	public void showIventory()
	{

	}

	public void hideInventory()
	{

	}

	public bool isinventoryHidden()
	{
		return true;
	}
}
