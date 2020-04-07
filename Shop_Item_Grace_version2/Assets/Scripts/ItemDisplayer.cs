using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDisplayer : MonoBehaviour
{
    protected string selectedItem;
    private GameObject item;

   
    public void selectItem()
    {
        //Get the GameObject that is being selected
		item = EventSystem.current.currentSelectedGameObject;
		if (item != null)
		{
            this.selectedItem = item.name;
            Debug.Log("Selected " + this.selectedItem);
          }
    }

    public void deselectItem(string itemName)
	{
        //if item is being selected, then deselect
		if (selectedItem==itemName)
		{
            Debug.Log("Deselected "+ itemName);
            EventSystem.current.SetSelectedGameObject(null);
            this.selectedItem = null;
        }
    }

    public string getSelectedItem()
	{
        return selectedItem;
	}
    public void Start()
    {
    }


     
}
