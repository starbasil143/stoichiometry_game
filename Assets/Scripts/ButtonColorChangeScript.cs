using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColorChangeScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Color selectedTextColor;
    private Color deselectedColor;

    public void Start()
    {
        deselectedColor = GetComponentInChildren<TMP_Text>().color;
    }

    public void OnSelect(BaseEventData eventData)
    {
        GetComponentInChildren<TMP_Text>().color = selectedTextColor;
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        GetComponentInChildren<TMP_Text>().color = deselectedColor;
    }
}

