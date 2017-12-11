using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelScript : MonoBehaviour {

    public GameObject winPanel;


	void Start () {
        winPanel.SetActive(false);
	}
	
    void displayWin()
    {
        winPanel.SetActive(true);
    }
}
