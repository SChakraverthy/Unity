using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;

public class NPCBehaviorTree2 : MonoBehaviour {

    public BehaviorAgent behaviorAgent;

    public GameObject player;
    public GameObject actor1;
    public GameObject actor2;

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

            new Sequence(
                
                this.ST_ApproachAndOrient(this.actor1, this.actor2),
                this.ST_EyeContactAndConversation(this.actor1, this.actor2)               
                
                )
            
            );

        return root;
    }

    protected Node ST_ApproachAndOrient(GameObject actor1, GameObject actor2)
    {

        Val<Vector3> a1Pos = Val.V(() => actor1.transform.position);
        Val<Vector3> a2Pos = Val.V(() => actor2.transform.position);
        Vector3 a2Dist = a2Pos.Value;

        return new Sequence(

            actor1.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(a2Dist, 2.5f),
            actor1.GetComponent<BehaviorMecanim>().Node_OrientTowards(a2Pos),
            actor2.GetComponent<BehaviorMecanim>().Node_OrientTowards(a1Pos)
            
            );
        
    }

    protected Node ST_EyeContactAndConversation(GameObject actor1, GameObject actor2)
    {

        return new Sequence(

            this.ST_EyeContact(actor1, actor2),
            this.ST_ConversationWithInterruption(actor1, actor2)
            
            );
        
    }

    protected Node ST_EyeContact(GameObject actor1, GameObject actor2)
    {
        Vector3 height = new Vector3(0.0f, 1.68f, 0.0f);
        Val<Vector3> a1Height = Val.V(() => actor1.transform.position + height);
        Val<Vector3> a2Height = Val.V(() => actor2.transform.position + height);

        return new SequenceParallel(
            
            actor1.GetComponent<BehaviorMecanim>().Node_HeadLook(a2Height),
            actor2.GetComponent<BehaviorMecanim>().Node_HeadLook(a1Height)
            
            );
    }

    protected Node ST_OrientAndEyeContact(GameObject actor1, GameObject actor2)
    {
        Val<Vector3> a1Pos = Val.V(() => actor1.transform.position);
        Val<Vector3> a2Pos = Val.V(() => actor2.transform.position);

        return new Sequence(
            actor1.GetComponent<BehaviorMecanim>().Node_OrientTowards(a2Pos),
            actor2.GetComponent<BehaviorMecanim>().Node_OrientTowards(a1Pos),
            this.ST_EyeContact(actor1,actor2)
            );
    }


    protected Node ST_ConversationWithInterruption(GameObject actor1, GameObject actor2)
    {
        return new Selector(
            
            this.ST_DetectAndReact(actor1, actor2),
            this.ST_Conversation(actor1, actor2)
            
            
            );
    }

    protected Node ST_Conversation(GameObject actor1, GameObject actor2)
    {
        return new Sequence(
            this.ST_OrientAndEyeContact(actor1, actor2),
            actor1.GetComponent<BehaviorMecanim>().ST_PlayConversationGesture("TALK1", 2000),
            actor2.GetComponent<BehaviorMecanim>().ST_PlayConversationGesture("TALK2", 2000),
            actor1.GetComponent<BehaviorMecanim>().ST_PlayConversationGesture("TALK2", 2000),
            actor2.GetComponent<BehaviorMecanim>().ST_PlayConversationGesture("TALK1", 2000)

            );
    }

    protected Node ST_DetectAndReact(GameObject actor1, GameObject actor2)
    {

        //return new DecoratorInvert(

            return new DecoratorLoop(

                new SelectorParallel(

                    this.AssertDetection(actor1, actor2),
                    this.AssertDetection(actor2, actor1)

                    )


                );
    }

    protected Node AssertDetection(GameObject actor, GameObject otherActor)
    {

        Vector3 height = new Vector3(0.0f, 1.85f, 0.0f);
        Val<Vector3> playerHeight = Val.V(() => player.transform.position + height);
        Val<Vector3> playerPos = Val.V(() => player.transform.position);

        return new Sequence(

            this.CheckDetected(actor1),
            actor.GetComponent<BehaviorMecanim>().Node_OrientTowards(playerPos),
            actor.GetComponent<BehaviorMecanim>().Node_HeadLook(playerHeight),
            otherActor.GetComponent<BehaviorMecanim>().Node_OrientTowards(playerPos),
            otherActor.GetComponent<BehaviorMecanim>().Node_HeadLook(playerHeight)

            );
    }

    protected Node CheckDetected(GameObject actor)
    {
        return new LeafAssert(() => PlayerDetected(actor));
    }

    bool PlayerDetected(GameObject actor)
    {

        Val<Vector3> origin = Val.V(() => actor.transform.position);
        Collider[] hitColliders = Physics.OverlapSphere(origin.Value, 2.0f);
        int i = 0;

        while (i < hitColliders.Length)
        {

            Collider currCollider = hitColliders[i];
            if (currCollider.CompareTag("Player"))
            {
                Debug.Log("Player Detected by " + actor.name + "!");
                return true;
            }

            i++;
        }

        return false;


    }
}
