using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuActions : MonoBehaviour
{
    public CityControl cityControl;
    public InputField nameText;
    public Button newGameButton;
    public Button loadGameButton;
    public Button returnMainMenuButton;
    public Button quitButton;
    public Slider sliderUI;
    public Text textSliderValue;
    public Dropdown savedGameDropdown;
    bool dropdownHidden;

    void Start()
    {
        ShowSliderValue();
        newGameButton.onClick.AddListener(delegate
        {
            this.gameObject.SetActive(false);
            string cityName = nameText.text;
            cityControl.setupNewGame(cityName, sliderUI.value);
        });
        loadGameButton.onClick.AddListener(delegate
        {
            if (dropdownHidden) return;
            // load selected file from disk
            CityState cityState = new CityState();
            cityState.sidewalks = new List<Node>();
            cityState.sidewalks.Add(new Node(1, 1));
            cityState.buildings = new List<Node>();
            cityState.sidewalks.Add(new Node(2, 2));
            cityState.citizens = new List<Node>();
            cityState.sidewalks.Add(new Node(3, 3));
            string filename = savedGameDropdown.options[savedGameDropdown.value].text;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/save_games/" + filename))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/save_games/" + filename, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), cityState);
                file.Close();
            }
            cityControl.LoadCity(cityState);
            this.gameObject.SetActive(false);
        });
        sliderUI.onValueChanged.AddListener(delegate
        {
            ShowSliderValue();
        });
        returnMainMenuButton.onClick.AddListener(delegate
        {
            loadSavedGameNames();
            this.gameObject.SetActive(true);
        });
        quitButton.onClick.AddListener(delegate
        {
            Application.Quit();
        });
        loadSavedGameNames();
    }


    void Update()
    {

    }

    public bool hasSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/save_games");
    }

    void loadSavedGameNames()
    {
        if (!hasSaveFile())
        {
            loadGameButton.interactable = false;
            savedGameDropdown.interactable = false;
            dropdownHidden = true;
        }
        var info = new DirectoryInfo(Application.persistentDataPath + "/save_games");
        var fileInfo = info.GetFiles();

        List<string> filenames = new List<string>();
        foreach (var file in fileInfo)
        {
            filenames.Add(file.Name);
        }

        savedGameDropdown.ClearOptions();
        savedGameDropdown.AddOptions(filenames);

        if (filenames.Count == 0)
        {
            loadGameButton.interactable = false;
            savedGameDropdown.interactable = false;
            dropdownHidden = true;
        }
        else
        {
            loadGameButton.interactable = true;
            savedGameDropdown.interactable = true;
            dropdownHidden = false;
        }
    }

    public void ShowSliderValue()
    {
        int minutes = (int)sliderUI.value / 60;
        int seconds = (int)sliderUI.value - minutes * 60;
        string sliderMessage = "";
        if(minutes > 0)
        {
            sliderMessage += minutes + "m ";
        }
        sliderMessage += seconds +"s";
        textSliderValue.text = sliderMessage;
    }



}
