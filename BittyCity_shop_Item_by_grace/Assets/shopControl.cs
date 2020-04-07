using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    private ShopScreen shopScreen;
    private ShopConents shopConents;
    public GameObject panel;
    private MeshRenderer myRenderer;


    public ShopControl()
	{
        shopConents = new ShopConents(new Dictionary<Item, int>(), 100);
        shopScreen = new ShopScreen();
	}
    public void loadShopScreen()
	{

	}
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("sj");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
