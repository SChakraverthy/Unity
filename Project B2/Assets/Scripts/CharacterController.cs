﻿using System.Collections;
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
        SPEED_MOD = KeyCode.LeftShift,

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
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var r = Input.GetAxis("Rotate");


        bool run = false;

        if (Input.GetKeyDown("left shift"))
        {
            run = true;
        }

        if (Input.anyKey == false) { animator.SetBool("move", false); }
        else
        {
            Move(x, y, r);
            animator.SetBool("move", true);
        }

    }

    void Move(float x, float y, float r)
    {
        animator.SetFloat("velx", x);
        animator.SetFloat("vely", y);
        transform.Rotate(0, r, 0);
        


        //if (run)
        //{

        transform.position += transform.forward * 10 * y * Time.deltaTime;
            transform.position += transform.right * 10 * x * Time.deltaTime;


        //    }

    }
}
