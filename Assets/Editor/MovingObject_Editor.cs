using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingObject))]
public class MovingObject_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MovingObject _target = (MovingObject)target;
        if (GUILayout.Button("AddCurrentLocation"))
        {
            _target.AddPosition();
        }
    }
}
