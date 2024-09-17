using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquationManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelection;
    [SerializeField] private GameObject MolButton;
    [SerializeField] private GameObject equationCanvas;


    private GameObject currentSelection;
    

    void Start()
    {

        #region Create Problem Lists
        //fill database of problems

        List<Problem> problemsTutorial = new List<Problem>
        {
            new Problem
            (
                new[] {new Molecule("O")},
                new[] {new Molecule("O 2")}
            ),
            new Problem
            (
                new[] {new Molecule("H 2"), new Molecule("O 2"), },
                new[] {new Molecule("H 2", "O")}
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
        #endregion

        EventSystem.current.SetSelectedGameObject(defaultSelection);
        currentSelection = EventSystem.current.currentSelectedGameObject;

        runProblem(problemsEasy[Random.Range(0,problemsEasy.Count-1)]);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelection);
        }

        currentSelection = EventSystem.current.currentSelectedGameObject;
    }

    void runProblem(Problem prob)
    {
        List<GameObject> buttonsLeft = new List<GameObject>();
        for (int i = 0; i < prob.leftSide.Count; i++)
        {
            buttonsLeft.Add(Instantiate(MolButton, equationCanvas.transform));
            buttonsLeft[i].GetComponent<MolButtonController>().molecule = prob.leftSide[i];
            buttonsLeft[i].transform.localPosition = new Vector2((i+1)*(-960f)/(prob.leftSide.Count+1), 0f);
        }

        List<GameObject> buttonsRight = new List<GameObject>();
        for (int i = 0; i < prob.rightSide.Count; i++)
        {
            buttonsRight.Add(Instantiate(MolButton, equationCanvas.transform));
            buttonsRight[i].GetComponent<MolButtonController>().molecule = prob.rightSide[i];
            buttonsRight[i].transform.localPosition = new Vector2((i+1)*(960f)/(prob.rightSide.Count+1), 0f);
        }
    }
}
