using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using Debug = UnityEngine.Debug;

/*
    Tabbed panel UI controller

    api:
        add new sheet
        delete sheet
        rename sheet




 */

public class TabbedPanel : MonoBehaviour
{
    //[SerializeField]
    //public TabbedSheet[] Sheets { get { return _sheetList.ToArray(); } }
    [SerializeField]
    public GameObject CurrentSheet
    {
        get { return _currentSelected.gameObject; }

        set
        {
            if (value == _currentSelected.gameObject)
            {
                Debug.Log("Same one.");
                return;
            }
            Debug.Log(string.Format("change {0} to {1}", _currentSelected.name, value.name));
            _currentSelected.gameObject.SetActive(false);
            _currentSelected = value.GetComponent<TabbedSheet>();
            _currentSelected.gameObject.SetActive(true);

        }
    }

    [SerializeField]
    //todo encapsulate this 
    public List<TabbedSheet> _sheetList = new List<TabbedSheet>();
    [SerializeField]
public List<Tab> _tabsList = new List<Tab>();
    
    //private GameObject _sheetPrefab ;
    private TabbedSheet _currentSelected;
    private RectTransform _tabsParent;
    private RectTransform _sheetsParent;

    void Awake()
    {
        //_sheetPrefab = GameObject.Find("Sheet");
        //todo this needs to be done at runtime or in the editor
        Debug.Assert(_tabsList.Count == _sheetList.Count, "Sheets count and Tabs count must match.");
        for (int i = 0; i < _tabsList.Count; i++)
        {
            Tab tab = _tabsList[i];
            TabbedSheet sheet = _sheetList[i];
            tab.OnTabClick.AddListener(HandleSheetOnTabClickEvent);
            tab.Label = sheet.name;
            if (i > 0)
            {
                sheet.gameObject.SetActive(false);
            }
            else
            {
                sheet.gameObject.SetActive(true);
            }
            
        }
        _currentSelected = _sheetList.First();

        foreach (RectTransform child in GetComponentsInChildren<RectTransform>())
        {
            if (child.name == "Tabs Parent")
            {
                _tabsParent = child;
            }
            else if (child.name == "Sheets Parent")
            {
                _sheetsParent = child;
            }
        }

        

    }

    void OnDestroy()
    {
        foreach (Tab tab in _tabsList)
        {
            tab.OnTabClick.RemoveListener(HandleSheetOnTabClickEvent);
        }
    }

    void HandleSheetOnTabClickEvent(Tab clickedTab)
    {
        CurrentSheet = _sheetList[clickedTab.transform.GetSiblingIndex()].gameObject;
        Debug.Log(clickedTab.name + " was clicked.");
    }


}

