using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_Cutscene : MonoBehaviour {

	public Camera[] cameras;
	public GameObject NPC;
	public GameObject NPC_Dead;
	//public GameObject mesh;

	private Dialogue lines;

	// Use this for initialization
	void Start () {
		lines = NPC.GetComponent<Dialogue> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (lines.lineIndex) {
		case 2:
			cameras [0].enabled = false;
			cameras [1].enabled = true;
			break;
		case 3:
			cameras [1].enabled = false;
			cameras [2].enabled = true;
			break;
		case 5:
			cameras [2].enabled = false;
			cameras [3].enabled = true;
			break;
		case 8:
			NPC.GetComponent<SkinnedMeshRenderer> ().enabled = false;
			NPC_Dead.GetComponent<SkinnedMeshRenderer> ().enabled = true;
			break;
		case 9:
			cameras [3].enabled = false;
			cameras [4].enabled = true;
			break;
		case 14:
			cameras [4].enabled = false;
			cameras [5].enabled = true;
			break;
		case 16:
			SceneManager.LoadScene ("b5hubtown");
			break;
		}

	}}
