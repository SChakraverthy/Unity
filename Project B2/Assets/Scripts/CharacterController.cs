using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    #region Enum

    public enum KEYBOARD_INPUT: int
    {

        FORWARDS = KeyCode.W,
        BACKWARDS = KeyCode.S,
        STRAFE_LEFT = KeyCode.A,
        STRAFE_RIGHT = KeyCode.D,

    }



    #endregion

    #region Vars

    Animator animator;

    #endregion


    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKey((KeyCode)KEYBOARD_INPUT.FORWARDS))
        {


            animator.SetFloat("moveZ", 0.5f);

        } else if (Input.GetKey((KeyCode)KEYBOARD_INPUT.BACKWARDS))
        {
            animator.SetFloat("moveZ", -0.5f);
        }
        else
        {
            animator.SetFloat("moveZ", 0f);
        }



	}
}
