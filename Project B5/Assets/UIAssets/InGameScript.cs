using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScript : MonoBehaviour {

    public GameObject InGamePanel;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (InGamePanel.activeInHierarchy)
            {
                InGamePanel.SetActive(false);
            }
            else
            {
                InGamePanel.SetActive(true);
            }
        }

    }

    public void LoadNewLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
