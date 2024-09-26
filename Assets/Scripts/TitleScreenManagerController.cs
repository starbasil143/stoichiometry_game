using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleScreenManagerController : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject currentSelection;

    void Start()
    {
        
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelection);
        }
        currentSelection = EventSystem.current.currentSelectedGameObject;
    }
}
