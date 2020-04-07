using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    private Item selectedItem;

    public ItemDisplayer()
	{

	}

    public void selectItem(Item selectedItem)
    {
        this.selectedItem = selectedItem;
    }

    public void deselectItem()
	{
        this.selectedItem = null;
	}

     
}
