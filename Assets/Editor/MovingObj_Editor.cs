using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingObj))]
public class MovingObj_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MovingObj _target = (MovingObj)target;
        if (GUILayout.Button("AddCurrentLocation"))
        {
            _target.AddPosition();
        }
    }
}
