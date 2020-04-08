using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventControl : MonoBehaviour
{
	public CityView cityView;
	public StructureControl structureControl;
	public GameObject bagItem1;
	public GameObject bagItem2;
	public GameObject bagItem3;
	public Transform inventoryPanel;
	public GameObject invDisplay;

	public static InventContent inventory = new InventContent();
	public static InventUI inventUI;
	public static ItemHolder itemHolder = new ItemHolder();

	public Button toggleInventory;
	private static Item restoreItem;

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
		itemHolder.generateItemPrices();
		if (itemInfo == null) return;
		Item item = itemHolder.itemConverter_inven(itemInfo);
		int itemID = item.getItemID();
		if (inventory.getItemAmounts(item) == 0) return; // :(
		switch (itemID)
		{
			case 1:
				//place Building
				if (cityView.itemApplication != null) cityView.itemApplication.cleanup();
				cityView.itemApplication = new BuildingItemApplication(structureControl);
				break;
			case 2:
				//place Sidewalk
				if (cityView.itemApplication != null) cityView.itemApplication.cleanup();
				cityView.itemApplication = new SidewalkItemApplication(structureControl);
				break;
			case 3:
				//place Bomb
				if (cityView.itemApplication != null) cityView.itemApplication.cleanup();
				cityView.itemApplication = new BombItemApplication(structureControl);
				break;
		}
		inventory.removeItem(item);
		restoreItem = item;
		toggleMeUp();


		//InventUI.displayApplySuccess();
	}

	public void showInventory()
	{
		inventUI.displayInventory();
	}

	public void hideInventory()
	{
		inventUI.hideInventory();
	}
	// Start is called before the first frame update
	void Start()
	{
		inventUI = new InventUI(bagItem1, bagItem2, bagItem3, inventoryPanel);
        
		/*
		inventory.addItem(new Item(1, "building", 100, "this is a building"));
		inventory.addItem(new Item(2, "sidewalk", 20, "this is a sidewalk"));
		inventory.addItem(new Item(2, "sidewalk", 20, "this is a sidewalk"));
		inventory.addItem(new Item(3, "bomb", 200, "this is a bomb"));
		*/    

		//inventUI.displayItems(InventControl.inventory.getInventoryDict());

		toggleInventory.onClick.AddListener(delegate
		{
			toggleMeUp();
			/*
			//inventUI.displayItems(InventControl.inventory.getInventoryDict());
			this.gameObject.SetActive(!this.gameObject.activeSelf);
			if (this.gameObject.activeSelf)
            {
				//Debug.Log("foo");
				//inventUI = new InventUI(bagItem1, bagItem2, bagItem3, inventoryPanel);
				inventUI.Clean();
				inventUI.displayItems(InventControl.inventory.getInventoryDict());
			}
			*/
			
		});
		invDisplay.SetActive(false);
		//this.gameObject.SetActive(false);
	}

	public void GameOver()
    {
		invDisplay.SetActive(false);
    }

	public void toggleMeUp()
    {
		//this.gameObject.SetActive(!this.gameObject.activeSelf);
		invDisplay.SetActive(!invDisplay.activeSelf);
		//if (this.gameObject.activeSelf)
		if (invDisplay.activeSelf)
		{
			inventUI.Clean();
			inventUI.displayItems(InventControl.inventory.getInventoryDict());
		}
	}



	public void tryToRestore()
    {
		if (cityView.itemApplication != null)
		{
			cityView.itemApplication.cleanup();
			cityView.itemApplication = null;
			inventory.addItem(restoreItem);
		}
	}


	// Update is called once per frame
	void Update()
	{
		//showInventory();
		/*
		if (Input.GetKey(KeyCode.Escape) && cityView.itemApplication != null)
		{
			cityView.itemApplication = null;
			inventory.addItem(restoreItem);
		}
		*/
	}
}
