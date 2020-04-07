using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private itemSprite sprite;
    private itemInfo info;

    public item(string name, string description)
	{
        this.info = new itemInfo(name, description);
        this.sprite = new itemSprite();
	}

    public string getName()
	{
        return this.info.getName();
	}

    public string getDescription()
	{
        return this.info.getDescription();
	}

    public void display(int x, int y)
	{
        this.sprite.display(x, y);
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

public class itemSprite : MonoBehaviour
{
    public itemSprite()
    {

    }

    public void display(int x, int y)
    {

    }
}

public class itemInfo : MonoBehaviour
{
    private string name;
    private string description;

    public itemInfo(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string getName()
    {
        return this.name;
    }

    public string getDescription()
    {
        return this.description;
    }

}

