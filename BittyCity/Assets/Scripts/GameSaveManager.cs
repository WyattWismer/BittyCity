using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;
    public CityState cityState;
    public CityControl cityControl;
    public CitizenControl citizenControl;
    public StructureControl structureControl;
    public CityMetricsUI metrics;

    //public Keybindings keybindings;

    /*
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Destory(this);
        }
        //DontDestroyOnLoad(this);
    }
    */



    public bool hasSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/save_games");
    }


    public void SaveGameTuples()
    {

    }

    public void SaveGame()
    {
        cityState = cityControl.SaveCity();
/*        cityState.test = new List<int>();
        cityState.test.Add(1);
        cityState.test.Add(2);
        cityState.test.Add(3);*/

        if (!hasSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save_games");
        }

        /*
        if (!Directory.Exists(Application.persistentDataPath + "/save_games/" + cityState.cityName));
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save_games/" + cityState.cityName);
        }
        */
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/save_games/" + cityState.cityName + "/" + cityState.cityName + ".json");
        FileStream file = File.Create(Application.persistentDataPath + "/save_games/" + cityState.cityName + ".json");
        var json = JsonUtility.ToJson(cityState);
        bf.Serialize(file, json);
        file.Close();
    }


    public void LoadGame()
    {
<<<<<<< HEAD
        /*        var info = new DirectoryInfo(Application.persistentDataPath + "/save_games");
                var fileInfo = info.GetFiles();
                foreach (var file in fileInfo)
                {
                    print(file);
                }*/
        CityState cityState2 = new CityState();
        if (!Directory.Exists(Application.persistentDataPath + "/save_games/" + metrics.getCityName()));
=======
/*        var info = new DirectoryInfo(Application.persistentDataPath + "/save_games");
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo)
        {
            print(file);
        }*/
        /*
        if (!Directory.Exists(Application.persistentDataPath + "/save_games/" + cityState.cityName)) ;
>>>>>>> fd0f4f2eae448472a04229a85709f8ceca6844d4
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save_games/" + cityState.cityName);
        }
        */
        BinaryFormatter bf = new BinaryFormatter();
<<<<<<< HEAD
        if (File.Exists(Application.persistentDataPath + "/save_games/" + metrics.getCityName() + "/" + metrics.getCityName() + ".json")) 
        {
            FileStream file = File.Open(Application.persistentDataPath + "/save_games/" + metrics.getCityName() + "/" + metrics.getCityName() + ".json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), cityState2);
=======
        if (File.Exists(Application.persistentDataPath + "/save_games/"  + cityState.cityName + ".json")) 
        {
            FileStream file = File.Open(Application.persistentDataPath + "/save_games/" + cityState.cityName + ".json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), cityState);
>>>>>>> fd0f4f2eae448472a04229a85709f8ceca6844d4
            file.Close();
        }
        cityControl.LoadCity(cityState2);
    }
}

