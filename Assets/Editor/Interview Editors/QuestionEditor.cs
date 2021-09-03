using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(Question))]
public class QuestionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Populate"))
        {
            Question question = (Question)target;
            question.PopulateQuestion();
        }
        else if (GUILayout.Button("Reset"))
        {
            Question question = (Question)target;
            question.ResetToDefaults();
        }
    }
}
