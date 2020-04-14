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

    private GameObject a, b, c;

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
                    
                    //GameObject obj = Instantiate(bagItem1, inventoryPanel);
                    if (a == null) a = Instantiate(bagItem1, inventoryPanel);
                    GameObject.Find("Canvas/InventoryPanel/RedBackground/building_Inven(Clone)/Amount").GetComponent<Text>().text = "Amount:" + inventoryDict[i];

                }
                else if (i == 2)
                {
                    //GameObject obj = Instantiate(bagItem2, inventoryPanel);
                    if (b == null) b = Instantiate(bagItem2, inventoryPanel);
                    GameObject.Find("Canvas/InventoryPanel/RedBackground/sidewalk_Inven(Clone)/Amount").GetComponent<Text>().text = "Amount:" + inventoryDict[i];

                }
                else if (i == 3)
                {
                    //GameObject obj = Instantiate(bagItem3, inventoryPanel);
                    if (c == null) c = Instantiate(bagItem3, inventoryPanel);
                    GameObject.Find("Canvas/InventoryPanel/RedBackground/bomb_Inven(Clone)/Amount").GetComponent<Text>().text = "Amount:" + inventoryDict[i];

                }
            }
            else
            {
                if (i == 1)
                {
                    if (a != null) Destroy(a);
                }
                else if (i == 2)
                {
                    if (b != null) Destroy(b);
                }
                else if (i == 3)
                {
                    if (c != null) Destroy(c);
                }
            }
        }
        
    }

    public void Clean()
    {
        /*
        if (a != null) Destroy(a);
        if (b != null) Destroy(b);
        if (c != null) Destroy(c);
        */
    }

    public void displayInventory() {
        SceneManager.LoadScene("Inventory");
    }

    public void hideInventory()
    {
        //change sence back to the Game screen
        SceneManager.LoadScene("SampleScene");
    }

    public static void displayApplySuccess()
    {
        SceneManager.LoadScene("ApplySucc");
    }

}

