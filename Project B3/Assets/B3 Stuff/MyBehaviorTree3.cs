using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree3 : MonoBehaviour
{
    public GameObject participant1;
    public GameObject participant2;
    public GameObject participant3;

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

    protected Node Dance(GameObject participant1, GameObject participant2)
    {

        Vector3 pos = participant2.transform.position;
        return new Sequence(this.participant1.GetComponent<BehaviorMecanim>().Node_OrientTowards(pos),
            this.participant1.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(pos, 2.0f),
            this.participant2.GetComponent<BehaviorMecanim>().Node_OrientTowards(participant1.transform.position),

            new SequenceParallel(

                new SequenceShuffle(
                this.participant1.GetComponent<BehaviorMecanim>().Node_Dance("Dance1"), this.participant1.GetComponent<BehaviorMecanim>().Node_Dance("Dance2"), this.participant1.GetComponent<BehaviorMecanim>().Node_Dance("Dance3")),

                new SequenceShuffle(
                this.participant2.GetComponent<BehaviorMecanim>().Node_Dance("Dance1"), this.participant2.GetComponent<BehaviorMecanim>().Node_Dance("Dance2"), this.participant2.GetComponent<BehaviorMecanim>().Node_Dance("Dance3"))
                ));

    }

    protected Node PlayGuitar(GameObject participant3)
    {

        return null;
    }

    protected Node ST_MeetAndDance(GameObject participant1, GameObject participant2, GameObject participant3)
    {

        return Dance(this.participant1, this.participant2);


    }

    protected Node BuildTreeRoot()
    {

        Node roaming = new DecoratorLoop(new Sequence(
            this.ST_MeetAndDance(this.participant1, this.participant2, this.participant3)));


        return roaming;
    }


    /* ST_ApproachAndWait Implementation:

    protected Node ST_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant1.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node BuildTreeRoot2()
    {
        Node roaming = new DecoratorLoop(
                        new SequenceShuffle(
                        this.ST_ApproachAndWait(this.wander1),
                        this.ST_ApproachAndWait(this.wander2),
                        this.ST_ApproachAndWait(this.wander3)));
        return roaming;
    }
    */
}
