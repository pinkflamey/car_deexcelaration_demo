using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public LevelController lc;

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public RectTransform arrow; // The arrow in the speedometer

    private float _speedKm = 0.0f;
    private float _maxSpeedKm = 0.0f; // The maximum speed of the target ** IN KM/H **

    private void Start()
    {
        _maxSpeedKm = lc.carMover.maxSpeedKm;
    }

    private void Update()
    {
        // 3.6f to convert in kilometers
        // ** The speed must be clamped by the car controller **
        _speedKm = lc.carMover.speedKm;

        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, _speedKm / _maxSpeedKm));
    }
}
