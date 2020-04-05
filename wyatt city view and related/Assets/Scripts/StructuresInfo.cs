using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresInfo : MonoBehaviour
{
    public GameObject structurePrefab;
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

    public void addStructure(int i, int j)
    {
        grid[i,j] = Instantiate(structurePrefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
    }

    public void removeStructure(int i, int j)
    {
        Destroy(grid[i, j]);
    }
}
