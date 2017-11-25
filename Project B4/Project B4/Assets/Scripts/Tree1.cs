using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class Tree1 : MonoBehaviour
{
    public GameObject waiter;
    public GameObject chef;

    private BehaviorAgent behaviorAgent;

    // Use this for initialization
    void Start()
    {

        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node ST_Converse(GameObject waiter, GameObject chef)
    {

        Vector3 chefPos = this.chef.transform.position;
        Vector3 waiterPos = this.waiter.transform.position;


        return new Sequence(this.waiter.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(chefPos, 10.0f), 
            this.waiter.GetComponent<BehaviorMecanim>().Node_OrientTowards(chefPos), 
            new LeafWait(500), this.chef.GetComponent<BehaviorMecanim>().Node_OrientTowards(waiterPos));

    }

    protected Node BuildTreeRoot()
    {
        Node conversation = new DecoratorLoop(new Sequence(this.ST_Converse(this.waiter, this.chef)));

        return conversation;
    }

}
