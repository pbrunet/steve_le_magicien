using UnityEngine;
using UnityEditor;

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(GeneratedCell))]
public class GeneratedCellEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        GeneratedCell cell = (GeneratedCell)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(cell.transform.position + cell.possibleExitPos, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(cell, "Change exit offset");
            cell.possibleExitPos = newTargetPosition - cell.transform.position;
        }
    }
}