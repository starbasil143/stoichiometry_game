using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListScript : MonoBehaviour
{
    [SerializeField] private GameObject SceneManagementController;
    [SerializeField] private GameObject EventSystemObject;

    public GameObject nextMessage;

    public float timer1;
    public float timer2;
    public int timerFlip = 1;
    private void Start()
    {
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");


        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        timer1 = 2.0f;

    }
    private void Update()
    {
        timer1 -= Time.deltaTime*timerFlip;

        if(timerFlip == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0f, timer1/1);
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, timer1/1);
            if(timer1 > 2f)
            {
                nextMessage.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        if(timer1 < -2f && timerFlip == 1)
        {
            timerFlip = -1;
            timer1 = -1;
        }



    }
}
