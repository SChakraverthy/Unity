using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
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
    public Text countText;
	public bool frozen;

    private int count;

    #endregion

    #region Controller



    // Use this for initialization
    void Start()
    {

        animator = GetComponent<Animator>();
		frozen = false;
        count = 0;
        SetCountText();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Teleport"))
        {
            GameObject manager = GameObject.Find("Button Manager");

            if (other.gameObject.name == "Teleport Future")
            {
                
                manager.GetComponent<InGameScript>().LoadNewLevel("b5futurezone");

            }

            if(other.gameObject.name == "Teleport Past")
            {

                manager.GetComponent<InGameScript>().LoadNewLevel("b5pastzone");

            }

            if(other.gameObject.name == "Teleport Hub")
            {
                manager.GetComponent<InGameScript>().LoadNewLevel("b5hubtown");
            }

        }
    }

    void SetCountText()
    {
        countText.text = count.ToString();
    }

    #endregion

}
