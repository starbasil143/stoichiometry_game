using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinScreenController : MonoBehaviour
{
    private Animator doorAnimator;
    public GameObject EquationManager;
    
    [SerializeField] private AudioSource AudioSlam;
    [SerializeField] private AudioSource AudioOpen;
    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        EquationManager = GameObject.FindWithTag("EquationManager");
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        
    }

    public void PlayLevelSwitchAnimation()
    {
        doorAnimator.Play("levelswitch");
    }

    public void GoToNextProblem()
    {
        if(EquationManager.GetComponent<EquationManager>() != null)
        {
            EquationManager.GetComponent<EquationManager>().RunNextProblem();
        }
        else
        {
            EquationManager.GetComponent<EquationManagerTutorial>().RunNextProblem();
        }
    }
   

    public void PlayAudioSlam()
    {
        AudioSlam.Play();
    }
    public void PlayAudioOpen()
    {
        AudioOpen.Play();
    }
}
