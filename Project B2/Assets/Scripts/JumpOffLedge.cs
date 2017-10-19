using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOffLedge : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        animator = GetComponent<animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

	// Update is called once per frame
	void Update () {
		if (agent.isOnOffmeshLink)
        {

        }
	}
}
