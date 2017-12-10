using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject InstructionsPanel;
    public GameObject LoadLevelPanel;
    public Transform dropDown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GetAndLoadScene()
    {
        int index = dropDown.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> options = dropDown.GetComponent<Dropdown>().options;
        string sceneName = options[index].text;

        if (sceneName == "Hub Town")
        {
            LoadNewScene("b5hubtown");

        } else if(sceneName == "Past Zone")
        {
            LoadNewScene("b5pastzone");

        } else if(sceneName == "Future Zone")
        {

            LoadNewScene("b5futurezone");

        }

        LoadNewScene(sceneName);
    }

    public void ShowInstructions()
    {
        InstructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        InstructionsPanel.SetActive(false);
    }

    public void ShowLoadLevel()
    {
        LoadLevelPanel.SetActive(true);
    }

    public void HideLoadLevel()
    {
        LoadLevelPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
