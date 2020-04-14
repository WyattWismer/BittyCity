﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityMetricsUI : MonoBehaviour
{
    public AchievementControl achievementControl;
    public Toggle displayMetricsToggle;
    public Text cityNameText;
    public Text timeRemainingText;
    public Text numCitizenText;
    public Text numSidewalkText;
    public Text numBuildingText;
    public Text numPointsText;

    private string cityName;
    private float timeRemaining; // measured in seconds
    private int numCitizens = 0;
    private int numSidewalks = 0;
    private int numBuildings = 0;
    private int numPoints = 0;


    // Start is called before the first frame update
    void Start()
    {
        displayCityName();
        displayTimeRemaining();
        displayNumCitizens();
        displayNumSidewalks();
        displayNumBuildings();
        displayNumPoints();

        // show/hide metrics
        displayMetricsToggle.onValueChanged.AddListener(delegate {
            this.gameObject.SetActive(displayMetricsToggle.isOn);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // city name methods
    public string getCityName()
    {
        return cityName;
    }

    public void setCityName(string name)
    {
        cityName = name;
        displayCityName();
    }

    public void displayCityName()
    {
        cityNameText.text = "Name: " + cityName;
    }

    // time remaining 
    public float getTimeRemaining()
    {
        return timeRemaining;
    }

    public void setTimeRemaining(float seconds)
    {
        timeRemaining = seconds;
        displayTimeRemaining();
    }

    public void displayTimeRemaining()
    {
        int minutes = (int) timeRemaining / 60;
        int seconds = (int) timeRemaining - minutes * 60;
        timeRemainingText.text = "Time: " + zeroBufferedInt(minutes) + ":" + zeroBufferedInt(seconds);
    }

    public string zeroBufferedInt(int x)
    {
        string res = x.ToString();
        if (x < 10)
        {
            res = "0" + res;
        }
        return res;
    }

    // citizen methods
    public int getNumCitizens()
    {
        return numCitizens;
    }

    public void setNumCitizens(int num)
    {
        numCitizens = num;
        displayNumCitizens();
        if (num == 10 || num == 100 || num == 250 || num == 500 || num == 1000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: " + num.ToString() + " citizens", "Earning this award means " + num.ToString() + " citizens have been created in your city");
        }

        if (num == 2000)
        {
            achievementControl.NotificationUpdate("Disgusting!", "You have created a city with 2000 citizens");
        }
    }

    private void displayNumCitizens()
    {
        numCitizenText.text = "Citizens: " + numCitizens.ToString();
    }

    // sidewalk methods
    public int getNumSidewalks()
    {
        return numSidewalks;
    }

    public void setNumSidewalks(int num)
    {
        numSidewalks = num;
        displayNumSidewalks();
        if (num == 10 || num == 100 || num == 250 || num == 500 || num == 1000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: " + num.ToString() + " sidewalks", "Earning this award means " + num.ToString() + " sidewalks have been built in your city");
        } 
    }

    private void displayNumSidewalks()
    {
        numSidewalkText.text = "Sidewalks: " + numSidewalks.ToString();
    }
    
    // building methods
    public int getNumBuildings()
    {
        return numBuildings;
    }

    public void setNumBuildings(int num)
    {
        numBuildings = num;
        displayNumBuildings();
        if (num == 10 || num == 100 || num == 250 || num == 500 || num == 1000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: " + num.ToString() + " buildings", "Earning this award means " + num.ToString() + " buildings have been built in your city");
        }
    }

    private void displayNumBuildings()
    {
        numBuildingText.text = "Buildings: " + numBuildings.ToString();
    }

    // point methods
    public int getNumPoints()
    {
        return numPoints;
    }

    public void setNumPoints(int num)
    {
        numPoints = num;
        displayNumPoints();
        if (num >= 1000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: Newbie", "Earned 1000 points");
        }

        if (num >= 5000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: Amateur", "Earned 5000 points");
        }

        if (num >= 10000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: Advanced", "Earned 10000 points");
        }

        if (num >= 50000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: Expert", "Earned 50000 points");
        }

        if (num >= 500000)
        {
            achievementControl.NotificationUpdate("Achievement Unlocked: God", "Earned 500000 points");
        }


    }

    private void displayNumPoints()
    {
        numPointsText.text = "Points: " + numPoints.ToString();
    }

    public void GameOver()
    {
        displayMetricsToggle.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        numCitizens = 0;
        numSidewalks = 0;
        numBuildings = 0;
        numPoints = 0;

        displayMetricsToggle.gameObject.SetActive(true);
        this.gameObject.SetActive(displayMetricsToggle.isOn);
        Start();
        achievementControl.Reset();
    }
}
