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
    public CameraSwitcher cs;
    [Header("Distance meter")]
    public Slider distanceSlider;
    public TextMeshProUGUI distanceText;
    [Header("Acceleration setting")]
    public TMP_InputField accelerationInputField;
    [Header("Buttons")]
    public UnityEngine.UI.Button runButton;
    public UnityEngine.UI.Button resetButton;
    [Header("Camera switching")]
    public TMP_Dropdown cameraDropdown;
    

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
        if (lc.carMover.transform.position.x > lc.resetPoint.position.x)
        {
            distanceSlider.value = 0;
            distanceText.text = "0";
        }
        else
        {
            distanceSlider.value = distance;
            distanceText.text = Mathf.Round(distance).ToString(); 
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
            var runColor = runButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            runButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(runColor.r, runColor.g, runColor.b, 0.5f);
            
            resetButton.interactable = true;
            var resetColor = resetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            resetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(resetColor.r, resetColor.g, resetColor.b, 1f);

            accelerationInputField.interactable = false;
        }
        else
        {
            runButton.interactable = true;
            var runColor = runButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            runButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(runColor.r, runColor.g, runColor.b, 1f);
            
            resetButton.interactable = false;
            var resetColor = resetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            resetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(resetColor.r, resetColor.g, resetColor.b, 0.5f);

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

    public void UpdateCamera()
    {
        cs.SwitchToCamera(cs.cameras[cameraDropdown.value]);
    }
}
