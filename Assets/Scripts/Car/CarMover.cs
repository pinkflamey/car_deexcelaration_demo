using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CarMover : MonoBehaviour
{
    public Transform resetPoint;

    public float startingSpeed;

    public float speed;
    public float acceleration;
    public float minSpeed;
    public float maxSpeed;

    public bool run;

    private void Start()
    {
        speed = startingSpeed;
    }

    private void Update()
    {
        if (run)
        {
            speed += acceleration * Time.deltaTime;
            
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

            if (speed < minSpeed)
            {
                speed = minSpeed;
            }

            GetComponent<Rigidbody2D> ().velocity = new Vector3(
                speed,
                GetComponent<Rigidbody2D> ().velocity.y
            );
        }

    }
}
