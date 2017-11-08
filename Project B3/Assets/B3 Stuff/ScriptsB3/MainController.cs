using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    #region Enum
    public enum KEYBOARD_INPUT : int
    {

        C_FORWARD = KeyCode.W,
        C_BACKWARDS = KeyCode.S,
        C_LEFT = KeyCode.A,
        C_RIGHT = KeyCode.D,
        C_UP = KeyCode.Space,
        C_DOWN = KeyCode.C,
        C_ROTATENEG = KeyCode.Q,
        C_ROTATEPOS = KeyCode.E,
        SPEED_MOD = KeyCode.LeftShift,
        B_RIGHT = KeyCode.RightArrow,
        B_LEFT = KeyCode.LeftArrow,
        B_FORWARD = KeyCode.UpArrow,
        B_BACKWARDS = KeyCode.DownArrow,

    }
    #endregion

    #region vars

    private Transform cameraTransform;
    private GameObject ball;
    private Rigidbody rb;
    public float speed_mod;
    public float ball_speed;


    #endregion

    #region Controller

    // Use this for initialization
    void Start () {

        cameraTransform = Camera.main.transform;
        ball = GameObject.Find("Ball");
        rb = ball.GetComponent<Rigidbody>();


    }
	
	// Update is called once per frame
	void Update () {

        // Handle the camera transforms.
        speed_mod = Input.GetKey((KeyCode)KEYBOARD_INPUT.SPEED_MOD) ? 5f : 1f;
        float rot_speed = 5f;
        ball_speed = 100f;

        foreach (KEYBOARD_INPUT val in Enum.GetValues(typeof(KEYBOARD_INPUT)))
        {

            if (Input.GetKey((KeyCode)val))
            {

                switch (val)
                {

                    case KEYBOARD_INPUT.C_FORWARD:

                        cameraTransform.position += (cameraTransform.forward * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_BACKWARDS:

                        cameraTransform.position -= (cameraTransform.forward * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_UP:
                        cameraTransform.position += (cameraTransform.up * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_DOWN:
                        cameraTransform.position -= (cameraTransform.up * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_RIGHT:
                        cameraTransform.position += (cameraTransform.right * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_LEFT:
                        cameraTransform.position -= (cameraTransform.right * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_ROTATENEG:
                        cameraTransform.Rotate(-Vector3.up * Time.deltaTime * rot_speed);
                        break;

                    case KEYBOARD_INPUT.C_ROTATEPOS:
                        cameraTransform.Rotate(Vector3.up * Time.deltaTime * rot_speed);
                        break;

                    case KEYBOARD_INPUT.B_FORWARD:
                        rb.AddForce(Vector3.forward * ball_speed * Time.deltaTime);
                        break;

                    case KEYBOARD_INPUT.B_BACKWARDS:
                        rb.AddForce(-Vector3.forward * ball_speed * Time.deltaTime);
                        break;

                    case KEYBOARD_INPUT.B_LEFT:
                        rb.AddForce(-Vector3.right * ball_speed * Time.deltaTime);
                        break;
                    case KEYBOARD_INPUT.B_RIGHT:
                        rb.AddForce(Vector3.right * ball_speed * Time.deltaTime);
                        break;
                }



            }

        }


    }

    #endregion
}
