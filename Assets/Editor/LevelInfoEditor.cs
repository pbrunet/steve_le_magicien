using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelInfo), true)]
public class LevelInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var picker = target as LevelInfo;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.sceneName);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("level", oldScene, typeof(SceneAsset), false) as SceneAsset;
        var levelName = EditorGUILayout.TextField("name", picker.friendlyName);

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("sceneName");
            scenePathProperty.stringValue = newPath;
            var sceneFriendlyNameProperty = serializedObject.FindProperty("friendlyName");
            sceneFriendlyNameProperty.stringValue = levelName;
        }
        serializedObject.ApplyModifiedProperties();
    }
}