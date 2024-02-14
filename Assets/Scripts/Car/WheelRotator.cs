using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    [Header("Settings")]
    public float sizeOfWheel = 1f;

    [Header("Calculated")]
    public float radius;
    public float speed;
    public float rotRad;
    public float rotDeg;
    
    private CarMover carMover;

    // Start is called before the first frame update
    void Start()
    {
        carMover = transform.parent.gameObject.GetComponent<CarMover>();
        
        radius = sizeOfWheel / 2;
    }

    /*
        speed * Δt = rotation * radius
        rotation = (speed * Δt) / radius
     */

    // Update is called once per frame
    void Update()
    {
        speed = carMover.GetComponent<Rigidbody2D>().velocity.x;

        rotRad = (speed * Time.deltaTime) / radius;
        rotDeg = rotRad * Mathf.Rad2Deg;

        transform.Rotate(0, 0, -rotDeg);


    }
}