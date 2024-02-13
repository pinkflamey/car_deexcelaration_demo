using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunButton : MonoBehaviour
{
    public TMP_InputField inputField;
    public CarMover carMover;

    public void Run()
    {
        float.TryParse(inputField.text, out carMover.acceleration);
        carMover.run = true;
    }
}
