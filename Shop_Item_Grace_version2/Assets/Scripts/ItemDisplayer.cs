using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDisplayer : MonoBehaviour
{
    public static string selectedItem;
    private GameObject item;
    private bool selected = false;
   
    public void selectItem(string itemName)
    {
		if (selectedItem == itemName)
		{
            selectedItem = null;
		}
		else
		{
           selectedItem = itemName;
        }
        Debug.Log(selectedItem);
    }


}
