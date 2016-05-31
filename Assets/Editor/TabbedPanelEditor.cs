using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(TabbedPanel))]
[CanEditMultipleObjects]
public class TabbedPanelEditor : Editor
{
    private List<TabbedSheet> _sheetList;
    private List<Tab> _tabList;
    private List<bool> _toggleStates;
    private TabbedPanel _tabbedPanel;
    private TabbedSheet _currentSheet;
    void OnEnable()
    {
        TabbedPanel _tabbedPanel = (TabbedPanel)target;

        if (_sheetList == null)
        {
            _sheetList = _tabbedPanel._sheetList;
            _currentSheet = _sheetList.First();

        }

        if (_tabList == null)
        {
            _tabList = _tabbedPanel._tabsList;
        }

        _toggleStates = new List<bool>(_sheetList.Count);
        for (int i = 0; i < _sheetList.Count; i++)
        {
            if (i != 0)
            {
                _toggleStates.Add(false);
            }
            else
            {
                _toggleStates.Add(true);
            }
        }

    }

    void OnDestroy()
    {
        //_isSheetsGroupExpanded.valueChanged.RemoveListener(Repaint);
    }
    public override void OnInspectorGUI()
    {
        foreach (TabbedSheet sheet in _sheetList)
        {
            Sheet(sheet);
        }

        if (GUILayout.Button("New Sheet"))
        {

        }

    }

    void Sheet(TabbedSheet sheet)
    {
        Image backgroundImage = sheet.gameObject.GetComponent<Image>();
        Sprite backgroundSprite = backgroundImage.sprite;
        Color backgroundColor = backgroundImage.color;
        int sheetIndex = _sheetList.IndexOf(sheet);
        Tab tab = _tabList[sheetIndex];
        Text tabText = tab.GetComponentInChildren<Text>();
        bool isCurrentSheet = sheet == _currentSheet;


        float prevWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 65;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(new GUIStyle() { fixedWidth = 25, alignment = TextAnchor.MiddleCenter });

        bool currentState = _toggleStates[sheetIndex];
        bool newState = EditorGUILayout.Toggle(currentState);
        if (newState != currentState)
        {
            if (!isCurrentSheet)
            {
                _toggleStates[sheetIndex] = newState;
                _toggleStates[_sheetList.IndexOf(_currentSheet)] = false;
                _currentSheet = sheet;
            }

        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        if (!isCurrentSheet)
        {
            GUI.enabled = false;
        }
        
        tabText.text = EditorGUILayout.TextField("Name:", tabText.text);
        
        tab.name = tabText.text;

        //Background image 
        EditorGUILayout.LabelField("Background Image");
        EditorGUI.indentLevel++;
        EditorGUILayout.BeginHorizontal();

        backgroundSprite = EditorGUILayout.ObjectField("Sprite", backgroundSprite, typeof(Sprite), true) as Sprite;

        backgroundColor = EditorGUILayout.ColorField("Color", backgroundColor);

        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;


        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = prevWidth;
        GUI.enabled = true;
    }
}

