using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CutsceneSkipper : MonoBehaviour
{
    public GameObject SceneManagementController;

    void Start()
    {
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.CheckAnswerInput)
        {
            SceneManagementController.GetComponent<Scene_Management_Controller>().GoToMenu();
        }
    }
}
