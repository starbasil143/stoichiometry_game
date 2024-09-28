using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class Scene_Management_Controller : MonoBehaviour
{
    private bool timerActive; 
    private float currentTime;
    public void Start()
    {
        Cursor.visible = false;
        timerActive = true;
        currentTime = 0;
    }
    public void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
    }
    public void StopTimer()
    {
        timerActive = false;
    }
    public string GetTime()
    {
        return String.Format("{0}:{1:00.00}", (int)currentTime/60, (float)currentTime%60);
    }
    public void GoToNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            WinGame();
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial_scene")
        {
            GoToMenu();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial_Scene");
    }
    public void PlaySoundtrack()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    public int GetLevel()
    {
        return SceneManager.GetActiveScene().buildIndex - 2; //easy is 0, med is 1, hard is 2
    }
    public bool InTutorial()
    {
        return SceneManager.GetActiveScene().ToString() == "Tutorial_Scene";
    }
    public void StopSoundtrack()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void DestroyUniverse()
    {
        SceneManager.LoadScene("GameOver_Scene");
    }
    public void WinGame()
    {
        SceneManager.LoadScene("Win_Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application quit");
    }

}

//SceneManager.LoadScene("SampleScene");
//brackeys



//increment molecules
//count elements
//u gotta do these things
//caus thats what the game is