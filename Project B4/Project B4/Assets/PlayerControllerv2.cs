using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerv2 : MonoBehaviour
{

    #region Enums

    public enum KEYBOARD_INPUT : int
    {

        P_FORWARD = KeyCode.W,
        P_BACKWARD = KeyCode.S,
        P_TURN_LEFT = KeyCode.A,
        P_TURN_RIGHT = KeyCode.D,
        SPEED_MOD = KeyCode.LeftShift,

    }

    #endregion

    #region Vars

    Animator animator;
	public bool frozen;

    #endregion

    #region Controller



    // Use this for initialization
    void Start()
    {

        animator = GetComponent<Animator>();
		frozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        reset();
		if (!frozen) {
			
			float speed_mod = Input.GetKey ((KeyCode)KEYBOARD_INPUT.SPEED_MOD) ? 2f : 1f;

			float x = Input.GetAxis ("Horizontal");
			float y = Input.GetAxis ("Vertical");
			//var r = Input.GetAxis("Rotate");

			foreach (KEYBOARD_INPUT val in Enum.GetValues(typeof(KEYBOARD_INPUT))) {

				if (Input.GetKey ((KeyCode)val)) {

					switch (val) {

					case KEYBOARD_INPUT.P_FORWARD:
						animator.SetBool ("Move", true);
						animator.SetFloat ("VelY", y * speed_mod);
						break;

					case KEYBOARD_INPUT.P_BACKWARD:
						animator.SetBool ("Move", true);
						animator.SetFloat ("VelY", y * speed_mod);
						break;

					case KEYBOARD_INPUT.P_TURN_LEFT:

						animator.SetBool ("Move", true);
						animator.SetFloat ("VelX", x * speed_mod);
						break;

					case KEYBOARD_INPUT.P_TURN_RIGHT:

						animator.SetBool ("Move", true);
						animator.SetFloat ("VelX", x * speed_mod);
						break;

					}           
                
				}
			}
		}

    }

    void reset()
    {
        animator.SetBool("Move", false);
        animator.SetFloat("VelX", 0f);
        animator.SetFloat("VelY", 0f);
    }

    #endregion

}
