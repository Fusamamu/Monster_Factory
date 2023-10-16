using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Monster
{
    [CustomEditor(typeof(AttackController))]
    public class AttackControllerEditor : Editor
    {
        private void OnSceneGUI()
        {
            AttackController attackController = (AttackController)target;
            Handles.color = Color.red;
            Handles.DrawWireArc(attackController.transform.position, Vector3.up, Vector3.forward, 360, attackController.AttackRange);
        }
    }
}
