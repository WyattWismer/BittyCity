using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitizenControl : MonoBehaviour
{
    public AchievementControl achievementControl;
    public CityMetricsUI metrics;
    public StructureControl structureControl;
    public GameObject citizenPrefab;
    public float heightOffset;
    public int numZombies = 0;
    public int numApocalypse = 0;

    private List<GameObject> citizens = new List<GameObject>();
    private Color citizenColor;
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

    public void setRandomCitizenColor()
    {
        citizenColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
    }

    public void addCitizen(int i, int j)
    {
        metrics.setNumCitizens(metrics.getNumCitizens() + 1);
        metrics.setNumPoints(metrics.getNumPoints() + 500);
        ShopControl.addToCurrency(25);
        GameObject newCitizen = Instantiate(citizenPrefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
        newCitizen.AddComponent<Citizen>();
        newCitizen.GetComponent<Citizen>().setup(this, structureControl, achievementControl, new Node(i, j));
        newCitizen.GetComponent<Renderer>().material.SetColor("_Color", citizenColor);
        citizens.Add(newCitizen);
    }

    public List<Node> getCitizens()
    {
        List<Node> result = new List<Node>();
        foreach(GameObject citizen in citizens)
        {
            if (citizen != null)
            {
                result.Add(citizen.GetComponent<Citizen>().location.Copy());
            }
        }
        return result;
    }

    public void Clean()
    {
        foreach (GameObject citizen in citizens)
        {
            if (citizen != null)
            {
                Destroy(citizen);
            }
        }
        metrics.setNumCitizens(0);
    }

    public void GameOver()
    {
        foreach (GameObject citizen in citizens)
        {
            if (citizen != null)
            {
                Destroy(citizen.GetComponent<Citizen>());
                citizen.AddComponent<MakeCitizenFlyAway>();
            }
        }
    }

    public void Reset()
    {

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

    public void killZombie(Citizen zombieCitizen)
    {
        GameObject zombie = zombieCitizen.gameObject;

        for (int i = 0; i < citizens.Count; i++)
        {
            if (citizens[i] != null && citizens[i].gameObject == zombie)
            {
                citizens[i] = null;
                Destroy(zombie.GetComponent<Citizen>());
                zombie.AddComponent<ZombieDeath>();
                metrics.setNumCitizens(metrics.getNumCitizens() - 1);
                return;
            }
        }
    }
}

public class ZombieDeath : MonoBehaviour
{
    void Start()
    {

    }

    public void Update()
    {
        // move rot amount
        Vector3 speed = new Vector3(0f, -1f, 0f) * Time.deltaTime;
        transform.Translate(speed, Space.World);

        // destroy when far enough off screen
        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }
}


