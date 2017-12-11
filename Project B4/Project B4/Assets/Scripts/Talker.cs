using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Talker : MonoBehaviour {

	public GameObject NPC;

	void Update(){
		this.transform.position = NPC.transform.position;
	}
}


