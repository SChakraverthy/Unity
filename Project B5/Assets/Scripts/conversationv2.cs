using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class conversationv2 : MonoBehaviour {

	public Text dialoguebox;
	PlayerController player;
	private GameObject talkingNPC;
	private Camera swapCamera;
	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		player = this.GetComponent<PlayerController> ();
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I) && !player.frozen) {
			Collider[] hitColliders = Physics.OverlapSphere (this.transform.position, 2.0f);

			int i = 0;

			while (i < hitColliders.Length) {
				if (hitColliders [i].tag == "NPC") {
					talkingNPC = hitColliders [i].gameObject;
					if (!(talkingNPC.GetComponent<Dialogue> ().lineIndex > talkingNPC.GetComponent<Dialogue> ().lineCount - 1)) {
						dialoguebox.text = talkingNPC.GetComponent<Dialogue> ().lines [talkingNPC.GetComponent<Dialogue> ().lineIndex];
						talkingNPC.GetComponent<Dialogue> ().lineIndex++;
						player.frozen = true;
						return;
					}
				} else if (hitColliders [i].tag == "camera") {

					swapCamera = hitColliders [i].gameObject.transform.parent.GetComponent<Camera> ();
					swapCamera.enabled = true;
					mainCamera.enabled = false;

					player.frozen = true;
					}
				i++;

				}
		} else if (Input.GetKeyDown (KeyCode.I) && player.frozen) {
			if (!mainCamera.enabled) {
				player.frozen = false;
				mainCamera.enabled = true;
				swapCamera.enabled = false;
			}
			else if (talkingNPC.GetComponent<Dialogue> ().lineIndex > talkingNPC.GetComponent<Dialogue> ().lineCount-1) {
				dialoguebox.text = "";
				player.frozen = false;
				talkingNPC.GetComponent<Dialogue> ().lineIndex = 0;
				talkingNPC = null;
			} else if (talkingNPC.GetComponent<Dialogue> ().lines [talkingNPC.GetComponent<Dialogue> ().lineIndex].Equals ("")) {
				dialoguebox.text = "";
				player.frozen = false;
				talkingNPC.GetComponent<Dialogue> ().lineIndex++;
				talkingNPC = null;
			} 
			else {
				dialoguebox.text = talkingNPC.GetComponent<Dialogue> ().lines [talkingNPC.GetComponent<Dialogue> ().lineIndex];
				talkingNPC.GetComponent<Dialogue> ().lineIndex++;
			}

				
		}

	}
}
