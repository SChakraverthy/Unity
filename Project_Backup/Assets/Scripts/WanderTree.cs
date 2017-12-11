using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;
using UnityEngine.AI;

public class WanderTree : MonoBehaviour {

    private BehaviorAgent behaviorAgent;
    //private GameObject currPoint;
    private bool wander;

    public GameObject actor;
    public GameObject wanderPoint;

    // Use this for initialization
	void Start () {

        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected Node BuildTreeRoot()
    {
        
        Node root = new DecoratorLoop(this.ST_Wander());
        return root;
    }
    
    protected Node ST_Wander()
    {
        Val<Vector3> newPos = Val.V(() => GenerateWaypoint());
        Debug.Log("The new position is: " + newPos.Value);
        return new Sequence(actor.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(newPos, 2.0f));

    }

    Vector3 GenerateWaypoint()
    {
        float range = 60.0f;

        Instantiate(wanderPoint);

		Vector3 center = new Vector3(-100.4f, 0.0f, -35.0f);
        Vector3 result;

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit navHit;

        for(int i = 0; i < 30; i++)
        {
            if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
            {
                result = navHit.position;

//                if (Physics.OverlapSphere(result, 1.0f).Length <= 2)
  //              {
                    wanderPoint.transform.position = result;
    //                break;
      //          }

            }
        }

        return wanderPoint.transform.position;
    }
}
