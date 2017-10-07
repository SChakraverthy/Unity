using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DynamicObs : MonoBehaviour {

    #region vars

    public Transform[] controlPoints;
    private Rigidbody rb;
    private int indexTarget;


    #endregion

    #region Dynamic Controller

    // Use this for initialization
    void Start () {

      


	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < controlPoints.Length; i++)
        {

            if (transform.position == controlPoints[i].position)
            {
                if (i == controlPoints.Length - 1)
                {
                    indexTarget = 0;

                }
                else
                {
                    indexTarget = i + 1;
                }
            }


        }


        //Debug.Log(message: "The index i is: " + indexTarget);
        Vector3 targetPos = controlPoints[indexTarget].position;
        float speed = 5f;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //Debug.Log(message: "The intended target is: " + targetPos + "The actual position is: " + transform.position);
           
	}
}

#endregion
