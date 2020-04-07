using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    protected Item selectedItem;

   
    public void selectItem(Item selectedItem)
    {
        this.selectedItem = selectedItem;
    }

    public void deselectItem()
	{
        this.selectedItem = null;
	}

    public Item getSelectedItem()
	{
        return selectedItem;
	}

     
}
