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

    public class Molecule
    {
        public List<(String eName, int eAmount)> elements = new List<(String eName, int eAmount)>();
        public int amount = 1;
        public Molecule(params string[] newElements) // Molecule("H,2", "0,1")
        {
            foreach (string elem in newElements)
            {
                string[] elemSplit = elem.Split(',');
                elements.Add((elemSplit[0],Convert.ToInt32(elemSplit[1])));
            }
        }

        public void Increment()
        {
            amount++;
        }
        public void Decrement()
        {
            amount--;
        }

        public List<(String eName, int eAmount)> GetElements()
        {
            List<(String eName, int eAmount)> totals = new List<(String eName, int eAmount)>();
            foreach ((String eName, int eAmount) element in elements)
            {
                totals.Add((element.eName, element.eAmount * amount));
            }
            return totals;
        }

        public override string ToString()
        {
            string output = "";
            foreach((String eName, int eAmount) element in elements)
            {
                output += element.eName + ((element.eAmount>1) ? element.eAmount : "");
            }
            return output;
        }
    }

    public class Problem
    {
        List<Molecule> leftSide = new List<Molecule>();
        List<Molecule> rightSide = new List<Molecule>();

        public void PushMolecule(string side, params Molecule[] molecules)
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
        
        public List<Molecule>[] GetMolecules()
        {
            List<Molecule>[] fullEquation = {leftSide, rightSide};
            return fullEquation;
        }

        public void PrintEquation()
        {
            string equationString = "";

            for (int i = 0; i < GetMolecules()[0].Count; i++) //left side
            {
                equationString += GetMolecules()[0][i].ToString() + " " + (i+1!=GetMolecules()[0].Count ? "+ " : "");
            }
            equationString += "--> ";
            for (int i = 0; i < GetMolecules()[1].Count; i++) //right side
            {
                equationString += GetMolecules()[1][i].ToString() + " " + (i+1!=GetMolecules()[1].Count ? "+ " : "");
            }
            
            Debug.Log(equationString);
        }

    }
    void Start()
    {
        Problem problem1 = new Problem();
        problem1.PushMolecule("left", new Molecule("N,2","H,2"));
        problem1.PushMolecule("right", new Molecule("N,3","H,1"));

        Problem problem2 = new Problem();
        problem2.PushMolecule("left",new Molecule("C,3","H,8"), new Molecule("O,2"));
        problem2.PushMolecule("right",new Molecule("H,2", "O,1"), new Molecule("C,1", "O,2"));

        Debug.Log("Fortnite");
        problem1.PrintEquation();
        problem2.PrintEquation();
    }

    void Update()
    {
        
    }
}

//SceneManager.LoadScene("SampleScene");
//brackeys



//increment molecules
//count elements
//u gotta do these things
//caus thats what the game is

