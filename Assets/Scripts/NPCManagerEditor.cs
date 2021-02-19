#if (UNITY_EDITOR) 
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCManager))]
[CanEditMultipleObjects]
public class NPCManagerEditor : Editor
{
    SerializedProperty npcDefinitions;
    SerializedProperty spawnPoints;

    SerializedProperty limit;
    bool positionsFold;

    void OnEnable()
    {
        limit = serializedObject.FindProperty("limit");
        npcDefinitions = serializedObject.FindProperty("npcDefinitions");
        spawnPoints = serializedObject.FindProperty("spawnPoints");
        SceneView.duringSceneGui += OnSceneGUI;

    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(limit);

        EditorGUILayout.PropertyField(npcDefinitions);

        EditorGUILayout.LabelField("Spawn Point Positions");
        EditorGUI.indentLevel++;
        var rect = EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("add", GUILayout.MaxWidth(50)))
        {
            spawnPoints.arraySize++;
            serializedObject.ApplyModifiedProperties();
            var element = spawnPoints.GetArrayElementAtIndex(spawnPoints.arraySize - 1);
            element.vector3Value = Vector3.zero;
        }
        EditorGUILayout.EndHorizontal();

        if (spawnPoints.arraySize > 0)
        {
            for (int i = spawnPoints.arraySize - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Vector3Field("", spawnPoints.GetArrayElementAtIndex(i).vector3Value);
                if (GUILayout.Button("x", GUILayout.MaxWidth(50)))
                {
                    spawnPoints.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndHorizontal();
            }

        }

        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI(SceneView sceneView)
    {
         var handles = new List<Vector3>();

         EditorGUI.BeginChangeCheck();

         for (int i = 0; i < spawnPoints.arraySize; i++)
         {
             handles.Add(Handles.PositionHandle(spawnPoints.GetArrayElementAtIndex(i).vector3Value, Quaternion.identity));
         }

         if (EditorGUI.EndChangeCheck())
         {
             Undo.RecordObject(target, "Move point");
             for (var i = 0; i < handles.Count; i++)
             {
                 spawnPoints.GetArrayElementAtIndex(i).vector3Value = handles[i];
             }
         }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif