using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresInfo : MonoBehaviour
{
    public GameObject structurePrefab;
    public GameObject transparentStructurePrefab;
    public GameObject deadStructurePrefab;
    public float heightOffset;
    private GameObject[,] grid = new GameObject[25, 25];


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject create(int i, int j, GameObject prefab)
    {
        if (grid[i, j] != null) removeStructure(i, j);
        return Instantiate(prefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
    }

    public void addFakeStructure(int i, int j)
    {
        grid[i, j] = create(i, j, transparentStructurePrefab);
    }

    public void addDeadStructure(int i, int j)
    {
        grid[i, j] = create(i, j, deadStructurePrefab);
    }

    public void addStructure(int i, int j)
    {
        grid[i, j] = create(i, j, structurePrefab);
    }

    public void removeStructure(int i, int j)
    {
        if (grid[i, j] != null) Destroy(grid[i, j]);
    }

    public List<Node> getStructures()
    {
        List<Node> result = new List<Node>();
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                if (grid[i, j] != null)
                {
                    result.Add(new Node(i, j));
                }
            }
        }
        return result;
    }

    public void GameOver(bool isSidewalk)
    {
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                if (grid[i, j] != null)
                {
                   // grid[i, j].AddComponent<MakeObjectFlyAway>();
                    if (isSidewalk)
                    {
                        grid[i, j].AddComponent<MakeSidewalkFlyAway>();
                    }
                    else
                    {
                        grid[i, j].AddComponent<MakeBuildingFlyAway>();
                    }

                }
            }
        }
    }

    public void Reset()
    {

    }

}
