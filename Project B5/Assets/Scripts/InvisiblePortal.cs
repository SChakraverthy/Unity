using UnityEngine;
using System.Collections;

public class InvisiblePortal : MonoBehaviour {
	public GameObject PortalDestination;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter (Collider other) {
        Vector3 displacement = other.transform.position - this.transform.position;

		other.transform.position = PortalDestination.transform.position;
		other.transform.position += displacement;
	}
}