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
    public float speedMeters;
    public float acceleration;
    public float minSpeedKm;
    public float maxSpeedKm;
    [Range(0.1f, 2f)] public float hitRadius;

    public State carState;

    private void Start()
    {
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        speedMeters = startingSpeedKm / 3.6f;
    }

    private void Update()
    {
        speedKm = speedMeters * 3.6f;
        
        if (carState == State.Running)
        {
            speedMeters += acceleration * Time.deltaTime;
            
            if (speedMeters > maxSpeedKm / 3.6f)
            {
                speedMeters = maxSpeedKm / 3.6f;
            }

            if (speedMeters < minSpeedKm / 3.6f)
            {
                speedMeters = minSpeedKm / 3.6f;
            }

            GetComponent<Rigidbody2D> ().velocity = new Vector3(
                speedMeters,
                GetComponent<Rigidbody2D> ().velocity.y
            );
            
            var hits = Physics2D.OverlapCircleAll(transform.position + new Vector3(2f, 0, 0), hitRadius);
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
        Gizmos.DrawWireSphere(transform.position + new Vector3(2f, 0, 0), hitRadius);
    }
}
