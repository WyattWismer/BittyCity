using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventUI : ItemDisplayer
{
    public static GameObject bagItem1;
    public static GameObject bagItem2;
    public static GameObject bagItem3;
    public static Transform inventoryPanel;

    public InventUI(GameObject bagItem1_new, GameObject bagItem2_new, GameObject bagItem3_new, Transform inventoryPanel_new)
    {
        bagItem1 = bagItem1_new;
        bagItem2 = bagItem2_new;
        bagItem3 = bagItem3_new;
        inventoryPanel = inventoryPanel_new;
    } 
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
                    
                    GameObject obj = Instantiate(bagItem1, inventoryPanel);
                    GameObject.Find("Canvas/InventoryPanel/RedBackground/building_Inven(Clone)/Amount").GetComponent<Text>().text = "Amount:" + inventoryDict[i];

                }
                else if (i == 2)
                {
                    GameObject obj = Instantiate(bagItem2, inventoryPanel);
                    GameObject.Find("Canvas/InventoryPanel/RedBackground/sidewalk_Inven(Clone)/Amount").GetComponent<Text>().text = "Amount:" + inventoryDict[i];

                }
                else if (i == 3)
                {
                    GameObject obj = Instantiate(bagItem3, inventoryPanel);
                    GameObject.Find("Canvas/InventoryPanel/RedBackground/bomb_Inven(Clone)/Amount").GetComponent<Text>().text = "Amount:" + inventoryDict[i];

                }
            }
        }
        
    }

    public void displayInventory() {
        SceneManager.LoadScene("Inventory");
    }

    public void hideInventory()
    {
        //change sence back to the original screen
    }

    public static void displayApplySuccess()
    {
        SceneManager.LoadScene("ApplySucc");
    }

}

