using System;
namespace InventControlApplication
{
	class InventControl
	{
		List<Item> InventList = new List<Item>();
		int ItemAmounts;

		public int getItemAmounts()
		{
			return ItemAmounts
		}

		public void addItem(Item i)
		{
			InventList.Add(i)
			ItemAmounts++;
		}

		public void removeItem(Item i)
		{
			InventList.Remove(i)
			ItemAmounts--;
		}
	}
}
