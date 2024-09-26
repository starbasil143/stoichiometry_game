using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultsScreenScript : MonoBehaviour
{
    private Animator resultsAnimator;
    public GameObject EquationManager;
    
    [SerializeField] private AudioSource AudioSlam;
    [SerializeField] private AudioSource AudioOpen;
    [SerializeField] private GameObject SceneManagerObject;

    private GameObject currentSelection;
    private bool resultsControlsActive;

    private void Start()
    {
    }

    private void OnEnable()
    {

        resultsAnimator = GetComponent<Animator>();
        EquationManager = GameObject.FindWithTag("EquationManager");
        SceneManagerObject = GameObject.FindWithTag("SceneManagementController");
        if(EquationManager.GetComponent<EquationManagerTutorial>() != null)
        {
            transform.Find("ResultsPanel").Find("ScoreText").GetComponent<TMP_Text>().text = "Score: " + EquationManager.GetComponent<EquationManagerTutorial>().score.ToString() + "/" + EquationManager.GetComponent<EquationManagerTutorial>().maxScore.ToString();
            transform.Find("ResultsPanel").Find("FailsafesText").GetComponent<TMP_Text>().text = "Failsafes remaining: " + (EquationManager.GetComponent<EquationManagerTutorial>().remainingLives-1).ToString();
        }
        else
        {
            transform.Find("ResultsPanel").Find("ScoreText").GetComponent<TMP_Text>().text = "Score: " + EquationManager.GetComponent<EquationManager>().score.ToString() + "/" + EquationManager.GetComponent<EquationManager>().maxScore.ToString();
            transform.Find("ResultsPanel").Find("FailsafesText").GetComponent<TMP_Text>().text = "Failsafes remaining: " + (EquationManager.GetComponent<EquationManager>().remainingLives-1).ToString();
        }
        transform.Find("ResultsPanel").Find("TimeText").GetComponent<TMP_Text>().text = "Time: " + SceneManagerObject.GetComponent<Scene_Management_Controller>().GetTime();

    
    }

    private void Update()
    {
        if (resultsControlsActive)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(currentSelection);
            }
            currentSelection = EventSystem.current.currentSelectedGameObject;
        }
        
    }

    public void PlayResultsAnimation()
    {
        resultsAnimator.Play("ResultsScreen");
    }

    public void GoToNextLevel()
    {
        SceneManagerObject.GetComponent<Scene_Management_Controller>().GoToNextLevel();
    }

    public void PlayAudioSlam()
    {
        AudioSlam.Play();
    }
    public void PlayAudioOpen()
    {
        AudioOpen.Play();
    }

    public void SwitchToResultsControls()
    {
        if(EquationManager.GetComponent<EquationManagerTutorial>() != null)
        {
            EquationManager.GetComponent<EquationManagerTutorial>().DisableButtons();
        }
        else
        {
            EquationManager.GetComponent<EquationManager>().DisableButtons();
        }
        EventSystem.current.SetSelectedGameObject(transform.Find("ResultsPanel").transform.Find("NextButton").gameObject);
        currentSelection = EventSystem.current.currentSelectedGameObject;
        resultsControlsActive = true;
    }

    
}
