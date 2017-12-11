using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;

public class WanderTree_2 : MonoBehaviour {

    private BehaviorAgent behaviorAgent;
    private int lastIndex;

    //public GameObject player;
    public GameObject actor;
    public Transform[] wanderPoints;

	private Vector3 location;

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
        Node root = new DecoratorLoop(

			this.ST_Wander(actor)
            
            );

        return root;
    } 

    protected Node ST_Wander(GameObject actor)
    {
		Val<Vector3> newPos = Val.V(() => GenerateWaypoint());
		Debug.Log("The new position is: " + newPos.Value);
		return new Sequence(new LeafWait(2000), actor.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(newPos, 2.0f),new LeafWait(2000));

  
    }

	Vector3 GenerateWaypoint()
	{
		int index = Random.Range(0, wanderPoints.Length);
		Debug.Log (index);

		location = wanderPoints[index].position;

		Debug.Log("The current point is: " + location);

		return location;
	}
}
