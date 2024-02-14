using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public UIController uiController;
    public Camera[] cameras;
    
    // Start is called before the first frame update
    void Start()
    {
        uiController.cameraDropdown.options.Clear();
        foreach (var cam in cameras)
        {
            uiController.cameraDropdown.options.Add(new TMP_Dropdown.OptionData(cam.gameObject.name));
        }
        SwitchToCamera(cameras[0]);
    }

    public void SwitchToCamera(Camera newCamera)
    {
        if (!cameras.Contains(newCamera)) return;
        foreach (var cam in cameras)
        {
            if (cam != newCamera) cam.enabled = false;
        }
        newCamera.enabled = true;
    }
}
