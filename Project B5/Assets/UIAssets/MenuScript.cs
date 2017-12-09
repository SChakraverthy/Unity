using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject InstructionsPanel;
    public GameObject LoadLevelPanel;

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
