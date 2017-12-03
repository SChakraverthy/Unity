using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour {

	public Text resource_counter; 

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(addResources);

	}
	
	void addResources(){
		resource_counter.text = (float.Parse (resource_counter.text) + 1).ToString ();
	}
}
