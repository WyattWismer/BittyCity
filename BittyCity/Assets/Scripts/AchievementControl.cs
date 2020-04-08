using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AchievementControl : MonoBehaviour
{
    public GameObject achievementWindow;
    public Text titleText;
    public Text messageText;
    public Queue<Tuple<string, string>> achievementQueue;
    public float counter = 5.0f;

    public void NotificationUpdate(string title, string message)
    {
        Tuple<string, string> temp = new Tuple<string, string>(title, message);
        achievementQueue.Enqueue(temp);

    }

    void Start()
    {
        achievementQueue = new Queue<Tuple<string, string>>();
    }


    void Update()
    {
        if (counter > 0 && achievementQueue.Count != 0)
        {
            Tuple<string, string> newVals = achievementQueue.Peek();
            titleText.text = newVals.Item1;
            messageText.text = newVals.Item2;
            achievementWindow.SetActive(true);
            counter -= Time.deltaTime;
        }
        else if (counter <= 0)
        {
            achievementQueue.Dequeue();
            achievementWindow.SetActive(false);
            counter = 5.0f;
        }

    }

}
