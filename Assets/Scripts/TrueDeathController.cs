using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueDeathController : MonoBehaviour
{

    [SerializeField] private GameObject SceneManagementController;

    private float duration;
    private void OnEnable()
    {
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");
        GetComponent<CanvasGroup>().alpha = 1;
        SceneManagementController.GetComponent<Scene_Management_Controller>().StopSoundtrack();
        duration = 2.0f;
    }
    private void Update()
    {
        duration -= Time.deltaTime;
        GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0f, duration/1);
        if(duration <= -2.0f)
        {
            SceneManagementController.GetComponent<Scene_Management_Controller>().DestroyUniverse();
        }
    }
}
