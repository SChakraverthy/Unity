using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    #region Enum

    public enum KEYBOARD_INPUT : int
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
    private bool run;
    private bool jump;

    #endregion


    // Use this for initialization
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var r = Input.GetAxis("Rotate");

        run = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;

        }

        if (Input.anyKey == false) { animator.SetBool("move", false); }
        else
        {

            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetTrigger("jump");

                if (animator.GetBool("move") == false)
                {
                    animator.SetBool("move", true);
                    animator.SetFloat("velx", 0f);
                    animator.SetFloat("vely", 0f);
                }

            }
            else
            {
                Move(x, y, r);
                animator.SetBool("move", true);
            }


        }



    }

    void Move(float x, float y, float r)
    {

        if (!run)
        {
            x = 0.5f * x;
            y = 0.5f * y;
        }

        animator.SetFloat("velx", x);
        animator.SetFloat("vely", y);


        transform.Rotate(0, r, 0);
        transform.position += transform.forward * 7 * y * Time.deltaTime;
        transform.position += transform.right * 7 * x * Time.deltaTime;

    }
}
