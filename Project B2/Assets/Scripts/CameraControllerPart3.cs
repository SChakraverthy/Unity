using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPart3 : MonoBehaviour
{

    #region Enum

    public enum KEYBOARD_INPUT : int
    {

        C_FORWARD = KeyCode.S,
        C_BACKWARDS = KeyCode.W,
        C_LEFT = KeyCode.D,
        C_RIGHT = KeyCode.A,
        SPEED_MOD = KeyCode.LeftShift,

    }

    #endregion

    #region Vars

    private Transform Camera_Transform;
    private float speed_mod;
    private float zoom_speed;

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

        // Camera XZ Movement
        speed_mod = Input.GetKey((KeyCode)KEYBOARD_INPUT.SPEED_MOD) ? 7f : 1f;
        zoom_speed = 15f;

        foreach (KEYBOARD_INPUT val in Enum.GetValues(typeof(KEYBOARD_INPUT)))
        {

            if (Input.GetKey((KeyCode)val))
            {

                switch (val)
                {

                    case KEYBOARD_INPUT.C_FORWARD:

                        Camera_Transform.Translate(Vector3.forward * speed_mod * Time.deltaTime, Space.World);
                        break;

                    case KEYBOARD_INPUT.C_BACKWARDS:

                        Camera_Transform.Translate(Vector3.back * speed_mod * Time.deltaTime, Space.World);
                        break;

                    case KEYBOARD_INPUT.C_RIGHT:

                        Camera_Transform.Translate(Vector3.right * speed_mod * Time.deltaTime, Space.World);
                        break;

                    case KEYBOARD_INPUT.C_LEFT:

                        Camera_Transform.Translate(Vector3.left * speed_mod * Time.deltaTime, Space.World);
                        break;

                }
            }
        }

        // Camera Y Movement
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta > 0f)
        {
            // Zoom In
            Camera_Transform.Translate(Vector3.down * zoom_speed * Time.deltaTime, Space.World);
        }

        if (zoomDelta < 0f)
        {
            //Zoom Out
            Camera_Transform.Translate(Vector3.up * zoom_speed * Time.deltaTime, Space.World);
        }
    }
    #endregion

}
