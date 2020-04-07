using System.Collections;

public class InventContent : ItemHolder
{
    private int ItemAmounts;

    public InventContent
    {
    	ItemAmounts = 0;
    }

	public int getItemAmounts()
	{
		return ItemAmounts;
	}

	public void addItem(Item i)
	{
		items.Add(i);
		ItemAmounts++;
	}

	public void removeItem(Item i)
	{
		items.Remove(i);
		ItemAmounts--;
	}
}
