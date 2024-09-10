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
        public Molecule(params string[] newElements) // Molecule("H 2", "0 1")
        {
            foreach (string elem in newElements)
            {
                string[] elemSplit = elem.Split(' ');
                if (elemSplit.Length == 1)
                {
                    elements.Add((elemSplit[0], 1));
                }
                else
                {
                    elements.Add((elemSplit[0],Convert.ToInt32(elemSplit[1])));
                }
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

        public Problem()
        {
            //empty constructor
        }

        public Problem(Molecule[] leftInput, Molecule[] rightInput)
        {
            leftSide.AddRange(leftInput);
            rightSide.AddRange(rightInput);
        }

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
        //fill database of problems

        List<Problem> problemsTutorial = new List<Problem>
        {
            new Problem
            (
                new[] {new Molecule("O 1")},
                new[] {new Molecule("O 2")}
            ),
            new Problem
            (
                new[] {new Molecule("H 2"), new Molecule("O 2"), },
                new[] {new Molecule("H 2", "O 1")}
            ),
            new Problem
            (
                new[] {new Molecule("Al 2", "O 3")},
                new[] {new Molecule("Al"), new Molecule("O 2"),}
            ),
        };

        List<Problem> problemsEasy = new List<Problem>
        {
            new Problem
            (
                new[] {new Molecule("N 2"), new Molecule("H 2")},
                new[] {new Molecule("N","H 3")}
            ),
            new Problem
            (
                new[] {new Molecule("Fe"), new Molecule("Cl 2")},
                new[] {new Molecule("Fe", "Cl 3")}
            ),
            new Problem
            (
                new[] {new Molecule("Fe"), new Molecule("O 2")},
                new[] {new Molecule("Fe 2", "O 3")}
            ),
            new Problem
            (
                new[] {new Molecule("Na"), new Molecule("Cl 2")},
                new[] {new Molecule("Na", "Cl")}
            ),
            new Problem
            (
                new[] {new Molecule("Al"), new Molecule("O 2")},
                new[] {new Molecule("Al 2", "O 3")}
            ),
            new Problem
            (
                new[] {new Molecule("K", "Cl", "O 3")},
                new[] {new Molecule("K", "Cl"), new Molecule("O 2")}
            ),
            new Problem
            (
                new[] {new Molecule("S 8"), new Molecule("O 2")},
                new[] {new Molecule("S", "O 2")}
            ),
            new Problem
            (
                new[] {new Molecule("Na", "Cl", "O 3")},
                new[] {new Molecule("Na", "Cl"), new Molecule("O 2")}
            ),
            new Problem
            (
                new[] {new Molecule("P 4"), new Molecule("O 2")},
                new[] {new Molecule("P 2", "O 5")}
            ),
            new Problem
            (
                new[] {new Molecule("Al"), new Molecule("Br 2")},
                new[] {new Molecule("Al", "Br 3")}
            ),
            new Problem
            (
                new[] {new Molecule("Xe"), new Molecule("F 2")},
                new[] {new Molecule("Xe", "F 6")}
            ),
            new Problem
            (
                new[] {new Molecule("Na", "H", "C", "O 3")},
                new[] {new Molecule("Na 2", "C", "O 3"), new Molecule("C", "O 2"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("N", "H 4", "Cl")},
                new[] {new Molecule("N", "H 3"), new Molecule("H", "Cl")}
            ),
            new Problem
            (
                new[] {new Molecule("Cr"), new Molecule("O 2")},
                new[] {new Molecule("Cr 2", "O 3")}
            ),
            new Problem
            (
                new[] {new Molecule("Cu", "Cl 3")},
                new[] {new Molecule("Cu"), new Molecule("Cl 2")}
            ),
            new Problem
            (
                new[] {new Molecule("V", "Cl 5")},
                new[] {new Molecule("V"), new Molecule("Cl 2")}
            ),
        };

        List<Problem> problemsMedium = new List<Problem>
        {
            new Problem
            (
                new[] {new Molecule("C 2", "H 4"), new Molecule("O 2")},
                new[] {new Molecule("C", "O 2"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("C 4", "H 10", "O"), new Molecule("O 2")},
                new[] {new Molecule("C", "O 2"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("K", "O", "H"), new Molecule("H 3", "P", "O 4")},
                new[] {new Molecule("K 3", "P", "O 4"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("Sn", "O 2"), new Molecule("H 2")},
                new[] {new Molecule("Sn"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("N", "H 3"), new Molecule("O 2")},
                new[] {new Molecule("N", "O"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("K", "N", "O 3"), new Molecule("H 2", "C", "O 3")},
                new[] {new Molecule("K 2", "C", "O 3"), new Molecule("H", "N", "O 3")}
            ),
            new Problem
            (
                new[] {new Molecule("Se", "Cl 6"), new Molecule("O 2")},
                new[] {new Molecule("Se", "O 2"), new Molecule("Cl 2")}
            ),
            new Problem
            (
                new[] {new Molecule("Fe", "O 3"), new Molecule("C", "O")},
                new[] {new Molecule("Fe"), new Molecule("C", "O 2")}
            ),
            new Problem
            (
                new[] {new Molecule("Al"), new Molecule("H", "Cl")},
                new[] {new Molecule("Al", "Cl 3"), new Molecule("H 2")}
            ),
            new Problem
            (
                new[] {new Molecule("H 3", "P", "O 4"), new Molecule("H", "Cl")},
                new[] {new Molecule("P", "Cl 5"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("Si", "Cl 4"), new Molecule("H 2", "O")},
                new[] {new Molecule("Si", "O 2"), new Molecule("H", "Cl")}
            ),
            new Problem
            (
                new[] {new Molecule("As"), new Molecule("Na", "O", "H")},
                new[] {new Molecule("Na 3", "As", "O 3"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("Au 2", "S 3"), new Molecule("H 2")},
                new[] {new Molecule("Au"), new Molecule("H 2", "S")}
            ),
            new Problem
            (
                new[] {new Molecule("V 2", "O 5"), new Molecule("H", "Cl")},
                new[] {new Molecule("V", "O", "Cl3"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("Si", "O 2"), new Molecule("H", "F")},
                new[] {new Molecule("Si", "F 4"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("H", "Cl", "O 4"), new Molecule("P 4", "O 10")},
                new[] {new Molecule("H 3", "P", "O 4"), new Molecule("Cl 2", "O 7")}
            ),
            new Problem
            (
                new[] {new Molecule("Na 3", "P", "O 4"), new Molecule("H", "Cl")},
                new[] {new Molecule("Na", "Cl"), new Molecule("H 3", "P", "O 4")}
            ),
            new Problem
            (
                new[] {new Molecule("Ti", "Cl 4"), new Molecule("H 2", "O")},
                new[] {new Molecule("Ti", "O 2"), new Molecule("H", "Cl")}
            ),
            new Problem
            (
                new[] {new Molecule("Ag", "N", "O 3"), new Molecule("K 3", "P", "O 4")},
                new[] {new Molecule("Ag 3", "P", "O 4"), new Molecule("K", "N", "O 3")}
            ),
        };

        foreach(Problem prob in problemsEasy)
        {
            prob.PrintEquation();
        }

                foreach(Problem prob in problemsMedium)
        {
            prob.PrintEquation();
        }

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

