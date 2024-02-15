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
    public Transform frontPoint;

    public float startingSpeedKm;

    public float speedKm;
    public float Speed { get; private set; }
    public float acceleration;
    public float minSpeedKm;
    public float maxSpeedKm;

    public State carState;

    private void Start()
    {
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        Speed = startingSpeedKm / 3.6f;
    }

    private void Update()
    {
        speedKm = Speed * 3.6f;
        
        if (carState == State.Running)
        {
            Speed += acceleration * Time.deltaTime;
            
            if (Speed > maxSpeedKm / 3.6f)
            {
                Speed = maxSpeedKm / 3.6f;
            }

            if (Speed < minSpeedKm / 3.6f)
            {
                Speed = minSpeedKm / 3.6f;
            }

            GetComponent<Rigidbody2D> ().velocity = new Vector3(
                Speed,
                GetComponent<Rigidbody2D> ().velocity.y
            );
            
            var hits = Physics2D.OverlapCircleAll(transform.position + new Vector3(2f, 0, 0), 1.5f);
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
        Gizmos.DrawWireSphere(transform.position + new Vector3(2f, 0, 0), 1.5f);
    }
}
