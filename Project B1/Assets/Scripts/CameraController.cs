using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Enum

    public enum KEYBOARD_INPUT : int
    {

        C_FORWARD = KeyCode.W,
        C_BACKWARDS = KeyCode.S,
        C_LEFT = KeyCode.A,
        C_RIGHT = KeyCode.D,
        C_UP = KeyCode.Space,
        C_DOWN = KeyCode.C,
        SPEED_MOD = KeyCode.LeftShift,
        X_ROTATE_NEG = KeyCode.I,
        X_ROTATE_POS = KeyCode.K,
        Y_ROTATE_NEG = KeyCode.J,
        Y_ROTATE_POS = KeyCode.L,

    }


    #endregion

    #region Vars

    private Transform Camera_Transform;
    private float speed_mod;

    #endregion

    #region Controller


    // Use this for initialization
    void Start()
    {

        Camera_Transform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {

        // Camera movement

        speed_mod = Input.GetKey((KeyCode)KEYBOARD_INPUT.SPEED_MOD) ? 10f : 5f;
        float rot_speed = 10f;

        foreach (KEYBOARD_INPUT val in Enum.GetValues(typeof(KEYBOARD_INPUT)))
        {

            if (Input.GetKey((KeyCode)val))
            {

                switch (val)
                {

                    case KEYBOARD_INPUT.C_FORWARD:

                        Camera_Transform.position += (Camera_Transform.forward * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_BACKWARDS:

                        Camera_Transform.position -= (Camera_Transform.forward * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_UP:
                        Camera_Transform.position += (Camera_Transform.up * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_DOWN:
                        Camera_Transform.position -= (Camera_Transform.up * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_RIGHT:
                        Camera_Transform.position += (Camera_Transform.right * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.C_LEFT:
                        Camera_Transform.position -= (Camera_Transform.right * Time.deltaTime * speed_mod);
                        break;

                    case KEYBOARD_INPUT.X_ROTATE_NEG:
                        Camera_Transform.Rotate(-Vector3.right * Time.deltaTime * rot_speed);
                        break;

                    case KEYBOARD_INPUT.X_ROTATE_POS:
                        Camera_Transform.Rotate(Vector3.right * Time.deltaTime * rot_speed);
                        break;

                    case KEYBOARD_INPUT.Y_ROTATE_NEG:
                        Camera_Transform.Rotate(-Vector3.up * Time.deltaTime * rot_speed);
                        break;

                    case KEYBOARD_INPUT.Y_ROTATE_POS:
                        Camera_Transform.Rotate(Vector3.up * Time.deltaTime * rot_speed);
                        break;
                }



            }

        }
    }

    #endregion
}
