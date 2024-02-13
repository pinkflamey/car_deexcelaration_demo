using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CarMover carMover;
    public Vector2 startPos;

    public static bool Reset { get; set; }

    private void Start()
    {
        startPos = new Vector2(carMover.boxBorder.position.x - 78, carMover.transform.position.y);
        carMover.transform.position = startPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset = true;
        }
        
        if (Reset)
        {
            Reset = !Reset;
            
            carMover.run = false;
            carMover.speed = carMover.startingSpeed;
            carMover.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carMover.acceleration = 0;
            carMover.transform.position = startPos;
        }
    }
}
