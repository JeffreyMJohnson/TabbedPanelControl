using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabbedSheet : MonoBehaviour
{
    [SerializeField]
    public Image BackgroundImage;

    void Awake()
    {
        BackgroundImage = GetComponent<Image>();
    }

}
