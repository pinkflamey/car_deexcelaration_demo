using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

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

    public void UpdateAccelerationSetting()
    {
        float parsedInput;
        float.TryParse(accelerationInputField.text, out parsedInput);

        lc.carMover.acceleration = parsedInput;
    }

    private void UpdateButtons()
    {
        if (lc.carMover.run == true)
        {
            runButton.interactable = false;
            resetButton.interactable = true;
        }
        else
        {
            runButton.interactable = true;
            resetButton.interactable = false;
        }
    }

    public void RunButton()
    {
        lc.carMover.run = true;
    }

    public void ResetButton()
    {
        lc.Reset = true;
    }
}
