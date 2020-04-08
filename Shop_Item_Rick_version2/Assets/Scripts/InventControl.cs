using System.Collections.Generic;
using UnityEngine;

public class InventControl : MonoBehaviour
{
	public GameObject bagItem1;
	public GameObject bagItem2;
	public GameObject bagItem3;
	public Transform inventoryPanel;

	public static InventContent inventory = new InventContent();
	public static InventUI inventUI;
	public static ItemHolder itemHolder = new ItemHolder();

	public void addItem(Item i)
	{
		inventory.addItem(i);
	}

	public void removeItem(Item i)
	{
		inventory.removeItem(i);
	}

	public void useItem()
	{
		string itemInfo = InventUI.selectedItem;
		Debug.Log(InventUI.selectedItem);
		if (itemInfo == null) return;
		Item item = itemHolder.itemConverter_inven(itemInfo);
		int itemID = item.getItemID();
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
		inventory.removeItem(item);

        InventUI.displayApplySuccess();
	}

	public void showInventory()
	{
		inventUI.displayItems(inventory.getInventoryDict());
	}

	public void hideInventory()
	{

	}
	// Start is called before the first frame update
	void Start()
	{
		inventUI = new InventUI(bagItem1, bagItem2, bagItem3, inventoryPanel);

		inventory.addItem(new Item(1, "building", 100, "this is a building"));
		inventory.addItem(new Item(2, "sidewalk", 20, "this is a sidewalk"));
		inventory.addItem(new Item(2, "sidewalk", 20, "this is a sidewalk"));
		inventory.addItem(new Item(3, "bomb", 200, "this is a bomb"));
		inventUI.displayItems(InventControl.inventory.getInventoryDict());
	}

	// Update is called once per frame
	void Update()
	{
		//showInventory();
	}
}
