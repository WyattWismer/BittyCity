using System;
using System.Collections.Generic;
using UnityEngine;

public class InventContent : MonoBehaviour
{
	public GameObject structurePrefab;
	public GameObject transparentStructurePrefab;
	public float heightOffset;
	private Object[,] grid = new Object[25, 25];


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	List<Item> InventList = new List<Item>();
	int ItemAmounts;

	public int getItemAmounts()
	{
		return ItemAmounts;
	}

	public void addItem(Item i)
	{
		InventList.Add(i);
		ItemAmounts++;
	}

	public void removeItem(Item i)
	{
		InventList.Remove(i);
		ItemAmounts--;
	}
}
