using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityControl : MonoBehaviour
{
    public CityMetricsUI metrics;
    public StructureControl structureControl;
    public CitizenControl citizenControl;
    public GameObject screenDimmer;
    public Text gameOverText;
    public Text gameOverPointsText;

    private bool hitGameOver;
    private float timeUntilGameOverText = 4;

    // Start is called before the first frame update
    void Start()
    {
        setupNewGame("TestCity", 90);
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
            if (!hitGameOver)
            {
                // game over
                citizenControl.GameOver();
                structureControl.GameOver();
                metrics.GameOver();
                screenDimmer.GetComponent<Image>().CrossFadeAlpha(1f, 4f, false);
                hitGameOver = true;

                gameOverText.text = "Game Over";
                gameOverPointsText.text = "You scored " + metrics.getNumPoints().ToString() + " points!";
            }
            else
            {
                if (timeUntilGameOverText <= 0)
                {
                    gameOverText.CrossFadeAlpha(1f, 2f, false);
                    gameOverPointsText.CrossFadeAlpha(1f, 2f, false);
                }
                else
                {
                    timeUntilGameOverText -= Time.deltaTime;
                }
            }

            return;
        }
        metrics.setTimeRemaining(currentTime);
    }

    public void setupNewGame(string cityName, float gameLengthInSeconds)
    {
        // make gameover screen invisible
        screenDimmer.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
        gameOverText.CrossFadeAlpha(0f, 0f, false);
        gameOverPointsText.CrossFadeAlpha(0f, 0f, false);
        hitGameOver = false;

        // set random citizen color
        citizenControl.setRandomCitizenColor();

        // set city properties
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

public abstract class MakeObjectFlyAway : MonoBehaviour
{
    private Vector3 speed;
    private Vector3 rotationSpeed;
    private Vector3 minSpeedChange, maxSpeedChange, minRotChange, maxRotChange;
    private float speedScaling;
    private float rotScaling;
    protected float globalScaling;

    protected void SharedStart()
    {
        speedScaling = 0.2f * globalScaling;
        rotScaling = 10f * globalScaling;
        speed = new Vector3(0f, 0f, 0f);
        rotationSpeed = new Vector3(0f, 0f, 0f);
        minSpeedChange = new Vector3(-1f, 0f, -1f) * speedScaling;
        maxSpeedChange = new Vector3(1f, 1f, 1f) * speedScaling;
        minRotChange = new Vector3(-1f, -1f, -1f) * rotScaling;
        maxRotChange = new Vector3(1f, 1f, 1f) * rotScaling;
    }

    public void Update()
    {
        // move rot amount
        transform.Translate(speed, Space.World);
        transform.Rotate(rotationSpeed);

        // change speed randomly
        for (int i = 0; i < 3; i++)
        {
            speed[i] += Random.Range(minSpeedChange[i], maxSpeedChange[i]) * Time.deltaTime;
        }

        // change rotation randomly
        for (int i = 0; i < 3; i++)
        {
            rotationSpeed[i] += Random.Range(minRotChange[i], maxRotChange[i]) * Time.deltaTime;
        }

        // destroy when far enough off screen
        if (transform.position.y >= 800)
        {
            Destroy(this.gameObject);
        }
    }
}

public class MakeSidewalkFlyAway : MakeObjectFlyAway
{
    void Start()
    {
        globalScaling = 0.3f;
        SharedStart();
    }
}

public class MakeBuildingFlyAway : MakeObjectFlyAway
{
    void Start()
    {
        globalScaling = 0.1f;
        SharedStart();
    }
}

public class MakeCitizenFlyAway : MakeObjectFlyAway
{
    void Start()
    {
        globalScaling = 0.8f;
        SharedStart();
    }
}




public class CityState
{
    public string cityName;
    public float timeRemaining;
    public List<Node> citizens, sidewalks, buildings;
    public int points;
    public CityState()
    {
    }
}
