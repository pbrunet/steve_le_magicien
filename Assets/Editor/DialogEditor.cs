using UnityEngine;
using UnityEditor;

// A tiny custom editor for ExampleScript component
[CustomEditor(typeof(Speech))]
public class DialogEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        Speech dialog = (Speech)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(dialog.transform.position + dialog.textOffset, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(dialog, "Change dialog offset");
            dialog.textOffset = newTargetPosition - dialog.transform.position;
        }
    }
}