using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventUI : ItemDisplayer
{
    public float heightOffset;
    public GameObject bagItem1;
    public GameObject bagItem2;
    public GameObject bagItem3;
    public Transform inventory;

    private List<Item> appliedItems;
    
    public void displayItems(Dictionary<int, int> inventoryDict)
    {
        //change sence to inventory
        //...

        // display items on the inventory panel
        foreach(int i in inventoryDict.Keys)
        {
            if (inventoryDict[i] > 0)
            {
                if (i == 1)
                {
                    GameObject obj = Instantiate(bagItem1, inventory);
                }
                else if (i == 2)
                {
                    GameObject obj = Instantiate(bagItem2, inventory);
                }
                else if (i == 3)
                {
                    GameObject obj = Instantiate(bagItem3, inventory);
                }
            }
        }
        
    }
    

    public void hideInventory()
    {
        //change sence back to the original screen
    }

    public static void displayApplySuccess()
    {
        SceneManager.LoadScene("ApplySucc");
    }

    public void applyItems(List<Item> appliedItems)
    {
        foreach (Item item in appliedItems)
        {
            InventControl.useItem();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        displayItems(InventControl.inventory.getInventoryDict());
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

