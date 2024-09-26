using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTimerScript : MonoBehaviour
{
    [SerializeField] private GameObject SceneManagementController;

    private float duration;
    private void OnEnable()
    {
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");
        duration = 2.0f;
    }
    private void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0f)
        {
            SceneManagementController.GetComponent<Scene_Management_Controller>().StartGame();
        }
    }
}
