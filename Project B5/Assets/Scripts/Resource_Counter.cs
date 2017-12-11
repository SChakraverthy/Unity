using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_Counter : MonoBehaviour {

	public Text resource_counter;
	public Text workerCount;
	public Text soldierCount;
	private float resourceCount;
	private float timeInterval;
	private float nextTime;

	void Start(){
		resourceCount = 0f;
		timeInterval = 1f;
		nextTime = 0f;
	}

	void Update(){
		if (Time.time >= nextTime) {
			countResources ();
			resource_counter.text = resourceCount.ToString ();
			nextTime += timeInterval;
		}
	}

	float soldierValue(){
		return float.Parse(soldierCount.text) * 3.5f;
	}

	float workerValue(){
		return float.Parse(workerCount.text) * 1.0f;
	}

	void countResources(){
		resourceCount = soldierValue ()
		+ workerValue ()
		+ float.Parse(resource_counter.text)
		;
	}
}
