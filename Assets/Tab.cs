using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerClickHandler
{

    public class OnTabClickEvent : UnityEvent<Tab> { };
    public OnTabClickEvent OnTabClick = new OnTabClickEvent();
    [SerializeField]
    public string Label
    {
        get { return _label.text; }
        set
        {
            _label.text = value;
            name = value;
        }
    }

    private Text _label;

    void Awake()
    {
        _label = GetComponentInChildren<Text>();
        Debug.Assert(_label != null, "Could not find Text child for tab.");
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnTabClick.Invoke(this);
        
    }
}
