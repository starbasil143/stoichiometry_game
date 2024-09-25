using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor.ProjectWindowCallback;

public class Scene_Management_Controller : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("stoich_game");
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