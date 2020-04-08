using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityControl : MonoBehaviour
{
    public CityMetricsUI metrics;
    public StructureControl structureControl;
    public CitizenControl citizenControl;

    // Start is called before the first frame update
    void Start()
    {
        setupNewGame("TestCity", 30);
    }

    // Update is called once per frame
    void Update()
    {
        // update time remaining
        float currentTime = metrics.getTimeRemaining();
        currentTime -= Time.deltaTime;
        if (currentTime < 0f) currentTime = 0f;
        if (currentTime == 0f)
        {
            // game over
        }
        metrics.setTimeRemaining(currentTime);
    }

    public void setupNewGame(string cityName, float gameLengthInSeconds)
    {
        metrics.setCityName(cityName);
        metrics.setTimeRemaining(gameLengthInSeconds);

        // initial structures
        structureControl.addBuilding(12, 12);
        structureControl.addSidewalk(12, 11);

        // initial citizens
        for(int i=0; i<2; i++)
        {
            citizenControl.addCitizen(12, 11);
        }
    }

    public CityState SaveCity()
    {
        CityState state = new CityState();
        state.cityName = metrics.getCityName();
        state.timeRemaining = metrics.getTimeRemaining();
        state.citizens = citizenControl.getCitizens();
        state.sidewalks = structureControl.getSidewalks();
        state.buildings = structureControl.getBuildings();
        state.points = metrics.getNumPoints();
        return state;
    }

    public void LoadCity(CityState state)
    {
        // clean up any existing stuff
        CleanCity();

        // load from saved state
        metrics.setCityName(state.cityName);
        metrics.setTimeRemaining(state.timeRemaining);
        foreach (Node node in state.sidewalks)
        {
            structureControl.addSidewalk(node.i, node.j);
        }
        foreach (Node node in state.buildings)
        {
            structureControl.addBuilding(node.i, node.j);
        }
        foreach (Node node in state.citizens)
        {
            citizenControl.addCitizen(node.i, node.j);
        }
        metrics.setNumPoints(state.points);
    }

    public void CleanCity()
    {
        citizenControl.Clean();
        structureControl.Clean();
    }
}

public class CityState
{
    //public List<int> test;
    public string cityName;
    public float timeRemaining;
    public List<Node> citizens, sidewalks, buildings;
    public int points;
    public CityState()
    {
    }
}
