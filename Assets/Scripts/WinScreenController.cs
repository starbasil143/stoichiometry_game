using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    Animator doorAnimator;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
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
}
