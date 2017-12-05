using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour {

    #region Vars

    private Animator animator;

    public Transform[] controlPoints;
    private Rigidbody rb;
    private int indexTarget;
    private NavMeshAgent agent;

    #endregion

    #region PatrolController
    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
        
	}

    void GoToNextPoint()
    {
        animator.SetBool("Move", true);

        if (controlPoints.Length == 0)
        {
            return;
        }

        agent.destination = controlPoints[indexTarget].position;
        indexTarget = (indexTarget + 1) % controlPoints.Length;
    }


    // Update is called once per frame
    void Update () {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
            
    }

    

    #endregion
}
