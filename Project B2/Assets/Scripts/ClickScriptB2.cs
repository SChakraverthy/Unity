using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScriptB2 : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    UnityEngine.AI.NavMeshAgent tempAgent;

    GameObject movObj;

    bool active;
    bool obstacleActive;

    Queue<UnityEngine.AI.NavMeshAgent> agentQueue;

    // Use this for initialization
    void Start()
    {
        active = false;
        obstacleActive = false;
        GameObject movObj = null;
        agentQueue = new Queue<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1) && !obstacleActive)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.tag == "agent" && hit.collider.gameObject.GetComponent<Renderer>().material.color != Color.blue)
                {
                    agent = hit.collider.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    agentQueue.Enqueue(agent);
                    agent.GetComponent<Renderer>().material.color = Color.blue;
                    active = true;
                }
                else if (Physics.Raycast(ray, out hit) && active && hit.collider.tag != "agent")
                {
                    while (agentQueue.Count > 0)
                    {
                        agent = agentQueue.Dequeue();                        
                        agent.GetComponent<Renderer>().material.color = Color.white;
                    }
                    active = false;
                }
            }
        }

        if(Input.GetMouseButtonDown(0) && !obstacleActive)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "agent" && hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.blue)
                {

                    agent = hit.collider.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    agent.GetComponent<Renderer>().material.color = Color.white;

                    tempAgent = agentQueue.Dequeue();

                    while (tempAgent != agent)
                    {
                        agentQueue.Enqueue(tempAgent);
                        tempAgent = agentQueue.Dequeue();
                    }

                    if (agentQueue.Count == 0)
                    {
                        active = false;
                    }
                }

                else if (Physics.Raycast(ray, out hit) && active && hit.collider.tag != "agent")
                {
                    while (agentQueue.Count > 0)
                    {
                        agent = agentQueue.Dequeue();
                        agent.destination = hit.point;
                        agent.GetComponent<Renderer>().material.color = Color.white;
                    }
                    active = false;
                }

            }   
            
        }
    }
}