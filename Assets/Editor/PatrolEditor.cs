using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static UnityEditor.PlayerSettings;

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(Patrol))]
public class PatrolEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        Patrol patrol = (Patrol)target;
        List<Vector2> newPositions = new List<Vector2>();

        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < patrol.waypoints.Count; i++)
        {
            Vector3 pos = new Vector3(patrol.waypoints[i].x, patrol.waypoints[i].y, 0);
            newPositions.Add(Handles.PositionHandle(pos, Quaternion.identity));
            Handles.Label(pos, "Waypoint" + i);
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(patrol, "Change waypoints offset");
            for (int i = 0; i < patrol.waypoints.Count; i++)
            {
                patrol.waypoints[i] = newPositions[i];
            }
        }
    }
}