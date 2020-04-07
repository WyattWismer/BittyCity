using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopScreen;

    public void ToggleShop()
    {
        if(ShopScreen != null)
        {
            bool currState = ShopScreen.activeSelf;

            ShopScreen.SetActive(!currState);
        }
    }
}
