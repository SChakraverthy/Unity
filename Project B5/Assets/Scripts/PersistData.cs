using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistData : MonoBehaviour {

    public bool pastComplete;
    public bool futureComplete;
    public GameObject player;
    public PlayerController pc;
    public GameObject winPanel;
    public WinPanelScript wps;

    private void Awake()
    {
        //winPanel.SetActive(false);
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start () {
        pastComplete = false;
        futureComplete = false;
        winPanel.SetActive(false);
	}
	

	void Update () {

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("b5pastzone") && pc.count == 5)
        {
            pastComplete = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("b5futurezone") && pc.count == 4)
        {
            futureComplete = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("b5hubtown") && pastComplete == true && futureComplete == true)
        {
            winPanel.SetActive(true);

        }
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        //winPanel = GameObject.Find("Win Panel");
        //winPanel.SetActive(false);
    }

}
