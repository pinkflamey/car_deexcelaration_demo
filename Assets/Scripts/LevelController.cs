using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CarMover carMover;
    public Vector2 startPos;

    public bool Reset { get; set; }

    private void Start()
    {
        startPos = new Vector2(carMover.resetPoint.position.x - 78, carMover.transform.position.y);
        carMover.transform.position = startPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) ||
            Vector2.Distance(carMover.transform.position, carMover.resetPoint.position) < 1f)
        {
            Reset = true;
        }
        
        if (Reset)
        {
            Reset = !Reset;
            
            carMover.run = false;
            carMover.speed = carMover.startingSpeed;
            carMover.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carMover.GetComponent<Rigidbody2D>().angularVelocity = 0;
            //carMover.GetComponent<Rigidbody2D>().inertia = 0;
            carMover.acceleration = 0;
            carMover.GetComponent<Rigidbody2D>().rotation = 0;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            carMover.transform.position = startPos;
        }
    }
}
