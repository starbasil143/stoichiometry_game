using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTitleScreenOnAwake : MonoBehaviour
{
    private GameObject SceneManagementController;
    void Awake()
    {
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");
        SceneManagementController.GetComponent<Scene_Management_Controller>().GoToMenu();
    }
}
