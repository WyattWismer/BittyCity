using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresInfo : MonoBehaviour
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

    private Object create(int i, int j, GameObject prefab)
    {
        return Instantiate(prefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
    }

    public Object createFakeStructure(int i, int j)
    {
        return create(i, j, transparentStructurePrefab);
    }

    public void addStructure(int i, int j)
    {
        grid[i, j] = create(i, j, structurePrefab);
    }

    public void removeStructure(int i, int j)
    {
        Destroy(grid[i, j]);
    }
}
