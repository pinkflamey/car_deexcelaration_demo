using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CarMover : MonoBehaviour
{
    public enum State { Waiting, Running }
    
    public LevelController lc;

    public float startingSpeed;

    public float speed;
    public float acceleration;
    public float minSpeed;
    public float maxSpeed;

    public State carState;

    private void Start()
    {
        speed = startingSpeed;
    }

    private void Update()
    {
        if (carState == State.Running)
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
            
            var hits = Physics2D.OverlapCircleAll(transform.position + new Vector3(2.5f, 0, 0), 1.5f);
            foreach (var c in hits)
            {
                if (!c.CompareTag("box")) return;
                if (lc.slowResetCoroutine != null) return;
                lc.slowResetCoroutine = StartCoroutine(lc.ResetSlow());
            }
        }

        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(2.5f, 0, 0), 1.5f);
    }
}
