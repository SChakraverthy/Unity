using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class nazgul : MonoBehaviour {

	public class collision2 : MonoBehaviour {

		UnityEngine.AI.NavMeshAgent nazgul;
		GameObject[] agents;

		// Use this for initialization
		void Start () {
			GameObject[] array = GameObject.FindGameObjectsWithTag ("Nazgul");
			nazgul = array [0].GetComponent<UnityEngine.AI.NavMeshAgent>();
			agents = GameObject.FindGameObjectsWithTag ("agent");

		}

		// Update is called once per frame
		void Update () {
			Vector3 nazGulPos = nazgul.transform.position;
			for (int i = 0; i < agents.Length; i++) {
				float distance = (agents[i].transform.position - nazGulPos).sqrMagnitude;
				if (distance <= 100.0f) {
					agents[i].GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = nazgul.velocity * (300.0f-distance);
					agents [i].GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 40;
				}
			}
		}
	}

}
