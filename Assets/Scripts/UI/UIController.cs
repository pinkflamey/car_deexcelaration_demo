using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Slider = UnityEngine.UI.Slider;

public class UIController : MonoBehaviour
{
    public LevelController lc;
    [Header("Distance meter")]
    public Slider distanceSlider;
    public TextMeshProUGUI distanceText;
    [Header("Acceleration setting")]
    public TMP_InputField accelerationInputField;
    [Header("Buttons")]
    public UnityEngine.UI.Button runButton;
    public UnityEngine.UI.Button resetButton;
    

    private void Start()
    {
        InitializeDistanceMeter();
    }

    private void Update()
    {
        UpdateDistanceMeter();
        UpdateButtons();
    }

    private void InitializeDistanceMeter()
    {
        distanceSlider.minValue = 0;
        distanceSlider.maxValue = Vector2.Distance(lc.carMover.frontPoint.position,
            lc.resetPoint.position);
    }

    private void UpdateDistanceMeter()
    {
        var distance = Vector2.Distance(lc.carMover.frontPoint.position,
            lc.resetPoint.position);
        if (lc.carMover.frontPoint.position.x >= lc.resetPoint.position.x)
        {
            distanceSlider.value = 0;
            distanceText.text = "0m";
        }
        else
        {
            distanceSlider.value = distance;
            distanceText.text = Mathf.Round(distance).ToString() + "m"; 
        }
    }

    public void UpdateAccelerationSetting()
    {
        float parsedInput;
        float.TryParse(accelerationInputField.text, out parsedInput);

        lc.carMover.acceleration = parsedInput;
    }

    private void UpdateButtons()
    {
        if (lc.carMover.carState == CarMover.State.Running)
        {
            runButton.interactable = false;
            
            resetButton.interactable = true;

            accelerationInputField.interactable = false;
        }
        else
        {
            runButton.interactable = true;
            
            resetButton.interactable = false;

            accelerationInputField.interactable = true;
        }
    }

    public void RunButton()
    {
        lc.carMover.carState = CarMover.State.Running;
    }

    public void ResetButton()
    {
        lc.Reset();
    }
}
