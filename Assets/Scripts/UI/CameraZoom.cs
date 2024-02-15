using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public LevelController lc;
    public Slider zoomSlider;

    private float sliderValue;

    private void Update()
    {
        if (sliderValue != zoomSlider.value)
        {
            var difference = sliderValue - zoomSlider.value;
            ZoomCenteredOnPointer(difference, lc.carMover.transform.position);
        }
        
        sliderValue = zoomSlider.value;
    }

    public void ZoomCenteredOnPointer(float zoomSteps, Vector3 pointerWorldPos)
    {
        float fracChange = (100.0f - 1 * zoomSteps) / 100;
        GetComponent<Camera>().orthographicSize *= fracChange;
        transform.position += (1 - fracChange) * (pointerWorldPos - transform.position);
    }
}
