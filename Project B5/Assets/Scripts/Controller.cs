﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS NOT USED IN B5 AT ALL

public class Controller : MonoBehaviour {

    public float moveSpeed = 6;

    Rigidbody rigidbody;
    Camera viewCamera;
    Vector3 velocity;


	void Start () {

        rigidbody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;

	}
	

	void Update () {

        Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
	}

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
