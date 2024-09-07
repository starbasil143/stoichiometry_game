using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class Scene_Management_Controller : MonoBehaviour
{

    public class Element
    {
        public string name;
        public int amount;

        public Element(string newName, int newAmount)
        {
            name = newName;
            amount = newAmount;
        }
    }

    public class Problem
    {
        List<string> leftSide = new List<string>();
        List<string> rightSide = new List<string>();

        public void PushMolecule(string side, params string[] molecules)
        {
            if(side.ToLower() == "left")
            {
                leftSide.AddRange(molecules);
            }
            else if(side.ToLower() == "right")
            {
                rightSide.AddRange(molecules);
            }
            else
            {
                Debug.Log("Invalid side. Use left or right.");
            }
        }
        
        public List<string>[] GetMolecules()
        {
            List<string>[] fullEquation = {leftSide, rightSide};
            return fullEquation;
        }

        public void PrintEquation()
        {
            string equationString = "";

            for (int i = 0; i < GetMolecules()[0].Count; i++) //left side
            {
                equationString += GetMolecules()[0][i] + " " + (i+1!=GetMolecules()[0].Count ? "+ " : "");
            }
            equationString += "--> ";
            for (int i = 0; i < GetMolecules()[1].Count; i++) //right side
            {
                equationString += GetMolecules()[1][i] + " " + (i+1!=GetMolecules()[1].Count ? "+ " : "");
            }
            
            Debug.Log(equationString);
        }

    }
    void Start()
    {
        Problem problem1 = new Problem();
        problem1.PushMolecule("left","N2","H2");
        problem1.PushMolecule("right","NH3");

        Problem problem2 = new Problem();
        problem2.PushMolecule("left","C3H8","O2");
        problem2.PushMolecule("right","H20","CO2");

        problem1.PrintEquation();
        problem2.PrintEquation();
    }

    void Update()
    {
        
    }
}

//SceneManager.LoadScene("SampleScene");
//brackeys