using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(TabbedPanel))]
[CanEditMultipleObjects]
public class TabbedPanelEditor : Editor
{
    private SerializedProperty Sheets;

    void OnEnable()
    {
        Sheets = serializedObject.FindProperty("Sheets");
    }
    public override void OnInspectorGUI()
    {
        if (Sheets != null)
        {
            EditorGUILayout.PropertyField(Sheets);
        }
        else
        {
            EditorGUILayout.LabelField("Null Sheets");
        }
        
    }
}

