using System.Collections;

public class shopContent : ItemHolder
{
    private int ItemAmounts;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
