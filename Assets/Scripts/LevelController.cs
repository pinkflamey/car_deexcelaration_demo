using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CarMover carMover;
    public Transform resetPoint;
    public Vector2 startPos;
    public GameObject boxesParent;
    public GameObject boxesPrefab;
    public float slowResetDelay;
    private Vector2 _boxesStartPosition;

    public Coroutine slowResetCoroutine;

    private void Start()
    {
        startPos = carMover.transform.position;
        carMover.transform.position = startPos;
        _boxesStartPosition = boxesParent.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        carMover.carState = CarMover.State.Waiting;
        carMover.speed = carMover.startingSpeed;
        carMover.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        carMover.GetComponent<Rigidbody2D>().angularVelocity = 0;
        carMover.acceleration = 0;
        carMover.GetComponent<Rigidbody2D>().rotation = 0;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        carMover.transform.position = startPos;
            
        Destroy(boxesParent);
        boxesParent = Instantiate(boxesPrefab, _boxesStartPosition, Quaternion.identity);

        if (slowResetCoroutine != null) StopCoroutine(slowResetCoroutine);
        slowResetCoroutine = null;
    }
    
    public IEnumerator ResetSlow()
    {
        yield return new WaitForSeconds(slowResetDelay);
        Reset();
        slowResetCoroutine = null;
    }
}
