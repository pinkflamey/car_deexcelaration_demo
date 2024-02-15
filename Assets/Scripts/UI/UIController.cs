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
    public string inputFieldText;
    public string inputFieldTextBlinking;
    [Header("Buttons")]
    public UnityEngine.UI.Button runButton;
    public UnityEngine.UI.Button resetButton;

    private Coroutine blinkInputFieldCoroutine;
    

    private void Start()
    {
        InitializeDistanceMeter();
        blinkInputFieldCoroutine = StartCoroutine(BlinkInputField(accelerationInputField));
    }

    private void Update()
    {
        UpdateDistanceMeter();
        UpdateButtons();
        if (blinkInputFieldCoroutine == null) StartCoroutine(BlinkInputField(accelerationInputField));
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
        if (lc.carMover.transform.position.x >= lc.resetPoint.position.x)
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
            /*var runColor = runButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            runButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(runColor.r, runColor.g, runColor.b, 0.5f);
            */
            
            resetButton.interactable = true;
            /*var resetColor = resetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            resetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(resetColor.r, resetColor.g, resetColor.b, 1f);
            */

            accelerationInputField.interactable = false;
        }
        else
        {
            runButton.interactable = true;
            /*var runColor = runButton.transform.GetChild(0).GetComponent<Image>().tintColor;
            runButton.transform.GetChild(0).GetComponent<Image>().tintColor = new Color(runColor.r, runColor.g, runColor.b, 1f);
            */
            
            resetButton.interactable = false;
            /*var resetColor = resetButton.transform.GetChild(0).GetComponent<Image>().tintColor;
            resetButton.transform.GetChild(0).GetComponent<Image>().tintColor = new Color(resetColor.r, resetColor.g, resetColor.b, 0.5f);
            */

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

    private IEnumerator BlinkInputField(TMP_InputField inputField)
    {
        yield return new WaitForSeconds(1f);
        inputField.placeholder.GetComponent<TextMeshProUGUI>().text = inputFieldTextBlinking;
        yield return new WaitForSeconds(1f);
        inputField.placeholder.GetComponent<TextMeshProUGUI>().text = inputFieldText;

        blinkInputFieldCoroutine = null;
    }
}
