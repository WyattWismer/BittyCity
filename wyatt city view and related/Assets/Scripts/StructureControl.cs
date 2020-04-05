using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureControl : MonoBehaviour
{
    public StructuresInfo sidewalkInfo;
    public StructuresInfo buildingInfo;

    // 0 is empty, 1 is sidewalk, 2 is building
    private int[,] grid = new int[25, 25];

    // Start is called before the first frame update
    void Start()
    {
        addSidewalk(0, 0);
        addSidewalk(1, 1);
        addSidewalk(2, 2);
        removeExisting(1, 1);

        addBuilding(10, 0);
        addBuilding(11, 1);
        addBuilding(12, 2);
        removeExisting(11, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addSidewalk(int i, int j)
    {
        sidewalkInfo.addStructure(i, j);
        grid[i, j] = 1;
    }

    void removeSidewalk(int i, int j)
    {
        sidewalkInfo.removeStructure(i, j);
        grid[i, j] = 0;
    }

    void addBuilding(int i, int j)
    {
        buildingInfo.addStructure(i, j);
        grid[i, j] = 2;
    }

    void removeBuilding(int i, int j)
    {
        buildingInfo.removeStructure(i, j);
        grid[i, j] = 0;
    }

    void removeExisting(int i, int j)
    {
        if(grid[i,j] == 1)
        {
            removeSidewalk(i, j);
        }
        else if (grid[i,j] == 2)
        {
            removeBuilding(i, j);
        }
    }

}
