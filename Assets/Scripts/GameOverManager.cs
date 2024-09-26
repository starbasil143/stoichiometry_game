using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject SceneManagementController;
    [SerializeField] private GameObject EventSystemObject;
    [SerializeField] private GameObject MessagePanel;
    [SerializeField] private GameObject SelectionsPanel;

    public float timer1;
    public float timer2;
    private GameObject currentSelection;
    private void Start()
    {
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");


        MessagePanel.GetComponent<CanvasGroup>().alpha = 0;
        timer1 = 2.5f;
        timer2 = 5.0f;

    }
    private void Update()
    {
        if(timer1>0)
            timer1 -= Time.deltaTime;

        MessagePanel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0f, timer1/1);

        if(timer2 > 0)
            timer2 -= Time.deltaTime;

        SelectionsPanel.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0f, timer2/1);

        if(timer2 <= 1.5f && !EventSystemObject.activeSelf)
        {
            EventSystemObject.SetActive(true);
        }


        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelection);
        }
        currentSelection = EventSystem.current.currentSelectedGameObject;
    }
}
