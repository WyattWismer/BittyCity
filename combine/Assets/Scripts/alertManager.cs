using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class alertManager : MonoBehaviour
{
	public void returnToShopScreen()
	{
		SceneManager.LoadScene(0);
	}
}
