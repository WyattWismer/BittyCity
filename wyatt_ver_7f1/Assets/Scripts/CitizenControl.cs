using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitizenControl : MonoBehaviour
{
    public CityMetricsUI metrics;
    public StructureControl structureControl;
    public GameObject citizenPrefab;
    public float heightOffset;

    // 0 - empty building, 1 - citizen waiting to mate, 2 - two citizens waiting to mate, 3 - mating done, one citizen left
    private int[,] matingGrid = new int[25, 25];


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCitizen(int i, int j)
    {
        metrics.setNumCitizens(metrics.getNumCitizens() + 1);
        metrics.setNumPoints(metrics.getNumPoints() + 500);
        GameObject new_citizen = Instantiate(citizenPrefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
        new_citizen.AddComponent<Citizen>();
        new_citizen.GetComponent<Citizen>().setup(this, structureControl, new Node(i, j));
    }

    // returns false if this isn't a valid mating location
    public bool tryToMate(int i, int j)
    {
        if (matingGrid[i,j] <= 1)
        {
            matingGrid[i,j]++;
            return true;
        }
        return false;
    }

    public void giveUpOnMating(int i, int j)
    {
        matingGrid[i, j] = 0;
    }

    public bool hasMated(int i, int j)
    {
        if(matingGrid[i,j] >= 2)
        {
            if (matingGrid[i,j] == 3)
            {
                addCitizen(i, j);
            }
            matingGrid[i, j] = (matingGrid[i, j] + 1) % 4; ;
            return true;
        }
        return false;
    }

    
}


