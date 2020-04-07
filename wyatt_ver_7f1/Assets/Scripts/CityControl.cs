using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityControl : MonoBehaviour
{
    public StructureControl structureControl;
    public CitizenControl citizenControl;

    // Start is called before the first frame update
    void Start()
    {
        setupNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setupNewGame()
    {
        structureControl.addBuilding(12, 12);
        // testing
        structureControl.addSidewalk(12, 11);
        //structureControl.addSidewalk(11, 11);
        //structureControl.addSidewalk(13, 11);
        /*
        structureControl.addSidewalk(10, 11);
        structureControl.addSidewalk(11, 10);
        structureControl.addSidewalk(13, 10);
        structureControl.addSidewalk(12, 9);
        structureControl.addSidewalk(11, 9);
        structureControl.addSidewalk(13, 9);
        */
        //
        for(int i=0; i<2; i++)
        {
            citizenControl.addCitizen(12, 11);
        }
        /*
        citizenControl.addCitizen(13, 11);
        citizenControl.addCitizen(12, 11);
        citizenControl.addCitizen(11, 11);
        citizenControl.addCitizen(13, 11);
        citizenControl.addCitizen(12, 11);
        citizenControl.addCitizen(11, 11);
        citizenControl.addCitizen(13, 11);
        citizenControl.addCitizen(12, 11);
        citizenControl.addCitizen(11, 11);
        */

    }
}
