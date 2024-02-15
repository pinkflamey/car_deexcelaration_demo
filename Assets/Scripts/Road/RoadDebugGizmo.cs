using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoadDebugGizmo : MonoBehaviour
{
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + new Vector3(0, 20, 0), transform.lossyScale.x.ToString());
    }
    #endif
}
