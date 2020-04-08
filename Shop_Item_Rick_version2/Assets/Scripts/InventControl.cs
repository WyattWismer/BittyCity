
public class InventControl
{
	public static InventContent inventory;
	private static InventUI inventUI;

	public InventControl()
	{
		inventory = new InventContent();
		inventUI = new InventUI();
	}

	public void addItem(Item i)
	{
		inventory.addItem(i);
	}

	public void removeItem(Item i)
	{
		inventory.removeItem(i);
	}

	public static void useItem(Item i)
	{
		int itemID = i.getItemID();
		switch (itemID)
		{
			case 0:
				//place Building
				break;
			case 1:
				//place Sidewalk
				break;
			case 2:
				//place Bomb
				break;
		}
		inventory.removeItem(i);
	}

	public void showIventory()
	{

		inventUI.displayItems(inventory.getInventoryDict());
	}

	public void hideInventory()
	{

	}
}
