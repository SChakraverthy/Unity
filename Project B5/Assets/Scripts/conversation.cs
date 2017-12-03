using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class conversation : MonoBehaviour {

	public Text dialoguebox;
	PlayerController player;
	private GameObject talkingNPC;

	// Use this for initialization
	void Start () {
		player = this.GetComponent<PlayerController> ();
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I) && !player.frozen) {
			Collider[] hitColliders = Physics.OverlapSphere (this.transform.position, 1.0f);

			int i = 0;

			while (i < hitColliders.Length) {
				if (hitColliders [i].tag == "NPC") {
					Debug.Log ("Collided");
					talkingNPC = hitColliders [i].gameObject;
					if (!(talkingNPC.GetComponent<Dialogue> ().lineIndex > talkingNPC.GetComponent<Dialogue> ().lineCount-1)) {
						dialoguebox.text = talkingNPC.GetComponent<Dialogue> ().lines [talkingNPC.GetComponent<Dialogue> ().lineIndex];
						talkingNPC.GetComponent<Dialogue> ().lineIndex++;
						player.frozen = true;
					}
					return;
				}
				i++;
			}
		} else if (Input.GetKeyDown (KeyCode.I) && player.frozen) {
			if (talkingNPC.GetComponent<Dialogue> ().lineIndex > talkingNPC.GetComponent<Dialogue> ().lineCount-1) {
				dialoguebox.text = "";
				player.frozen = false;
				talkingNPC = null;
			} else if (talkingNPC.GetComponent<Dialogue> ().lines [talkingNPC.GetComponent<Dialogue> ().lineIndex].Equals ("")) {
				dialoguebox.text = "";
				player.frozen = false;
				talkingNPC.GetComponent<Dialogue> ().lineIndex++;
				talkingNPC = null;
			} else {
				dialoguebox.text = talkingNPC.GetComponent<Dialogue> ().lines [talkingNPC.GetComponent<Dialogue> ().lineIndex];
				talkingNPC.GetComponent<Dialogue> ().lineIndex++;
			}

				
		}

	}
}
