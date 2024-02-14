using System;
using UnityEngine;

public class CarAccelerator : MonoBehaviour
{

    public enum Modes
    {
        Start,
        Accelerating
    };

    public Modes modeSwitch;

    public Vector2 startPosition;
    public Vector2 startForce;
    public Vector2 acceleration;

    public float minXForce, maxXForce;
    
    private Rigidbody2D _rb;

    private bool _started;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        modeSwitch = Modes.Start;

        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        switch (modeSwitch)
        {
            case Modes.Start:
                _started = false;
                transform.position = startPosition;
                _rb.velocity = new Vector2(0, 0);
                
                break;
            
            case Modes.Accelerating:
                if (!_started) _rb.velocity = startForce;
                _started = true;

                if (Mathf.Round(_rb.velocity.x) < minXForce)
                {
                    _rb.velocity = new Vector2(minXForce, -_rb.velocity.x);
                }

                if (Mathf.Round(_rb.velocity.x) > maxXForce)
                {
                    _rb.velocity = new Vector2(maxXForce, _rb.velocity.y);
                }
                
                _rb.AddForce(acceleration, ForceMode2D.Force);
                
                break;
        }
    }
}