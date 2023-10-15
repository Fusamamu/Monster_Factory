using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Monster
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            FieldOfView fieldOfView = (FieldOfView)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.ViewRadius);
        }
    }
}
