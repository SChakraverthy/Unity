using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class collisionscript : MonoBehaviour
{

    //Possibilities: check collisions with nearby objects
    //if an object is nearby, compute the normal for each agent object, and move to the right
    //this should enable vector calculations each frame that create a circle
    //if an agent's destination is the same as another agent's destination, choose the one closer to the goal
    //update the farther one's speed to be slightly slower for some time
    //change destination to behind closer agent so when one agent reaches its goal the next will stop short
    //
    //issues: will agent still move toward destination?
    //issues: how much time will an agent need to avoid collision?
    NavMeshAgent agent;

    bool onTravel;

    Vector3 initialgoal;

    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        collisionscript cf = gameObject.GetComponent<collisionscript>();
    }

    void OnCollisionEnter(Collision otherObject)
    {
        if (otherObject.gameObject.tag == "agent" && otherObject.gameObject.GetComponent<NavMeshAgent>().remainingDistance < .9f)
        {
            agent.destination = agent.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1f && agent.speed > 0)
        {
            agent.destination = agent.transform.position;
        }
        //	if (agent.remainingDistance > 0.7f) {
        //		onTravel = true;
        //	}
        //	if(agent.remainingDistance < .4f && onTravel){
        //		agent.speed = 0;
        //		print ("Enteredasda");

        //		onTravel = false;
    }
}
