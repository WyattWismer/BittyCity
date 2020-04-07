using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventUI : itemHolder
{

	private Object create(int i, int j, GameObject prefab)
    {
        return Instantiate(prefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
    }

    public Object createFakeStructure(int i, int j)
    {
        return create(i, j, transparentStructurePrefab);
    }

	public void displayItems()
	{

	}

	public Item applyItems(List<Item> appliedItems){
		for 
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

