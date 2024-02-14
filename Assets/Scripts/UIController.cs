using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public LevelController lc;
    public Slider distanceSlider;
    public TextMeshProUGUI distanceText;

    private void Start()
    {
        InitializeDistanceMeter();
    }

    private void Update()
    {
        UpdateDistanceMeter();
    }

    private void InitializeDistanceMeter()
    {
        distanceSlider.minValue = 0;
        distanceSlider.maxValue = Vector2.Distance(lc.carMover.transform.position,
            lc.carMover.resetPoint.position);
    }

    private void UpdateDistanceMeter()
    {
        var distance = Vector2.Distance(lc.carMover.transform.position,
            lc.carMover.resetPoint.position);
        distanceSlider.value = distance;
        distanceText.text = Mathf.Round(distance).ToString();
    }
}
