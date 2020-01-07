using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Events;

[CustomEditor(typeof(EventTester))]
public class EventTesterEditor  : Editor
{
    SerializedProperty e;

    private void OnEnable()
    {
        e = serializedObject.FindProperty("e");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(e);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("push me"))
        {
            ((EventTester)(serializedObject.targetObject)).Invoke();
            
        }
    }
}