using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquationManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelection;
    [SerializeField] private GameObject MolButton;
    [SerializeField] private GameObject equationCanvas;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject ErrorFlash;
    [SerializeField] private GameObject ResultsScreen;
    [SerializeField] private GameObject SceneManagementController;
    [SerializeField] private GameObject Failsafe;
    [SerializeField] private GameObject TrueDeath;

    private int[] problemSetAmounts = {5,5,5};
    public int maxScore;

    public int remainingLives;
    public int score = 0;
    public int fails = 0;


    private GameObject currentSelection;

    private Problem currentProblem;

    
    private List<Problem> easyProblemSet = new List<Problem>();
    private List<Problem> medProblemSet = new List<Problem>();
    private List<Problem> hardProblemSet = new List<Problem>();


    private List<List<Problem>> ProblemSets = new List<List<Problem>>(); //picked out problems
    private List<List<Problem>> ProblemDatabase = new List<List<Problem>>(); //all problems

    
    private List<GameObject> buttonsLeft = new List<GameObject>();
    private List<GameObject> buttonsRight = new List<GameObject>();

    private int currentProblemSet = 0;
    private int currentProblemNumber = 0;
    private bool buttonsEnabled;
    private int currentLevel;
    

    private void Start()
    {

        maxScore = problemSetAmounts[currentProblemSet] * 100;
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");
        currentLevel = SceneManagementController.GetComponent<Scene_Management_Controller>().GetLevel();
        currentProblemSet = currentLevel;
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
                new[] {new Molecule("Na"), new Molecule("Cl 2")},
                new[] {new Molecule("Na", "Cl")}
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
                new[] {new Molecule("Na 3", "As", "O 3"), new Molecule("H 2")}
            ),
            new Problem
            (
                new[] {new Molecule("Au 2", "S 3"), new Molecule("H 2")},
                new[] {new Molecule("Au"), new Molecule("H 2", "S")}
            ),
            new Problem
            (
                new[] {new Molecule("V 2", "O 5"), new Molecule("H", "Cl")},
                new[] {new Molecule("V", "O", "Cl 3"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("Si", "O 2"), new Molecule("H", "F")},
                new[] {new Molecule("Si", "F 4"), new Molecule("H 2", "O")}
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
        };
        List<Problem> problemsHard = new List<Problem>
        {
            new Problem
            (
                new[] {new Molecule("Ag", "N", "O 3"), new Molecule("K 3", "P", "O 4")},
                new[] {new Molecule("Ag 3", "P", "O 4"), new Molecule("K", "N", "O 3")}
            ),
            new Problem
            (
                new[] {new Molecule("H 3", "P", "O 4"), new Molecule(1, "Mg(OH)<sub>2</sub>", "Mg", "O 2", "H 2")},
                new[] {new Molecule(1, "Mg<sub>3</sub>(PO<sub>4</sub>)<sub>2</sub>", "Mg 3", "P 2", "O 8"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule(1, "Al(OH)<sub>3</sub>", "Al", "O 3", "H 3"), new Molecule("H 2", "C", "O 3")},
                new[] {new Molecule(1, "Al<sub>2</sub>(CO<sub>3</sub>)<sub>3</sub>", "Al 2", "C 3", "O 9"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule(1, "Ca<sub>3</sub>(PO<sub>4</sub>)<sub>2</sub>", "Ca 3", "P 2", "O 8"), new Molecule("Si", "O 2"), new Molecule("C")},
                new[] {new Molecule("Ca", "Si", "O 3"), new Molecule("C", "O"), new Molecule("P 4")}
            ),
            new Problem
            (
                new[] {new Molecule("H 3", "P", "O 4"), new Molecule(1, "Ca(OH)<sub>2</sub>", "Ca", "O 2", "H 2")},
                new[] {new Molecule(1, "Ca<sub>3</sub>(PO<sub>4</sub>)<sub>2</sub>", "Ca 3", "P 2", "O 8"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule(1, "Al(OH)<sub>3</sub>", "Al", "O 3", "H 3"), new Molecule("H", "Br")},
                new[] {new Molecule("Al", "Br 3"), new Molecule("H 2", "O")}
            ),
            new Problem
            (
                new[] {new Molecule("Fe", "Cl 3"), new Molecule("Na", "O", "H")},
                new[] {new Molecule(1, "Fe(OH)<sub>3</sub>", "Fe", "O 3", "H 3"), new Molecule("Na", "Cl")}
            ),
            new Problem
            (
                new[] {new Molecule(1, "Pb(OH)<sub>2</sub>", "Pb", "O 2", "H 2"), new Molecule("H", "Cl")},
                new[] {new Molecule("H 2", "O"), new Molecule("Pb", "Cl 2")}
            ),
            new Problem
            (
                new[] {new Molecule("Al", "Br 3"), new Molecule("K 2", "S", "O 4")},
                new[] {new Molecule("K", "Br"), new Molecule(1, "Al<sub>2</sub>(SO<sub>4</sub>)<sub>3</sub>", "Al 2", "S 3", "O 12")}
            ),
            new Problem
            (
                new[] {new Molecule(1, "Cu(NO<sub>3</sub>)<sub>2</sub>", "Cu", "N 2", "O 6"), new Molecule("K", "O", "H")},
                new[] {new Molecule(1, "Cu(OH)<sub>2</sub>", "Cu", "O 2", "H 2"), new Molecule("K", "N", "O 3")}
            ),
            new Problem
            (
                new[] {new Molecule(1, "Mn(NO<sub>2</sub>)<sub>2</sub>", "Mn", "N 2", "O 4"), new Molecule("Be", "Cl 2")},
                new[] {new Molecule(1, "Be(NO<sub>2</sub>)<sub>2</sub>", "Be", "N 2", "O 4"), new Molecule("Mn", "Cl 2")}
            ),
            new Problem
            (
                new[] {new Molecule("H", "Cl", "O 4"), new Molecule("P 4", "O 10")},
                new[] {new Molecule("H 3", "P", "O 4"), new Molecule("Cl 2", "O 7")}
            ),
        };

        

        // fill ProblemDatabase
        ProblemDatabase.Add(problemsEasy);
        ProblemDatabase.Add(problemsMedium);
        ProblemDatabase.Add(problemsHard);

        // fill ProblemSets
        ProblemSets.Add(easyProblemSet);
        ProblemSets.Add(medProblemSet);
        ProblemSets.Add(hardProblemSet);
        #endregion

        buttonsEnabled = true;
        for (int i = 0; i < ProblemSets.Count; i++)
        {
            for (int j = 0; j < problemSetAmounts[i]; j++)
            {
                int rand = Random.Range(0, ProblemDatabase[i].Count-1);
                ProblemSets[i].Add(ProblemDatabase[i][rand]);
                ProblemDatabase[i].RemoveAt(rand);
            }
        }



        runProblem(ProblemSets[currentLevel][currentProblemNumber]);

    }

    private void Update()
    {
        if(buttonsEnabled)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(currentSelection);
            }
            currentSelection = EventSystem.current.currentSelectedGameObject;


            if (InputManager.instance.MoleculeIncrementInput)
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<MolButtonController>().IncrementMolecule();
            }
            if (InputManager.instance.MoleculeDecrementInput)
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<MolButtonController>().DecrementMolecule();
            }
            if (InputManager.instance.CheckAnswerInput)
            {
                    Debug.Log(currentProblem.isBalanced());
                if(WinScreen.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "levelswitch" && WinScreen.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "levelstart" && !ResultsScreen.activeSelf)
                {
                    if(currentProblem.isBalanced())
                    {
                        if(currentProblemNumber < (problemSetAmounts[currentProblemSet]-1))
                        {
                            
                            score+=100;
                            WinScreen.GetComponent<WinScreenController>().PlayLevelSwitchAnimation();
                        }
                        else
                        {
                            score+=100;
                            ResultsScreen.SetActive(true);
                        }
                    }
                    else
                    {
                        remainingLives--;
                        fails++;
                        if(remainingLives == 0)
                        {
                            TrueDeath.SetActive(false);
                            TrueDeath.SetActive(true);
                        }
                        else if (remainingLives > 0)
                        {
                            ErrorFlash.SetActive(false);
                            ErrorFlash.SetActive(true);
                        }
                        score-=30;
                        Failsafe.GetComponent<Failsafe_Container_Manager>().SpendFailsafe();
                    }
                }
            }
        }
    }



    public void RunNextProblem()
    {
        DestroyButtons();

        if(currentProblemNumber < (problemSetAmounts[currentProblemSet]-1))
        {
            runProblem(ProblemSets[currentProblemSet][++currentProblemNumber]);
        }
    }

    public void DestroyButtons()
    {
        foreach (GameObject item in buttonsLeft)
        {
            Destroy(item);
        }
        foreach (GameObject item in buttonsRight)
        {
            Destroy(item);
        }
        buttonsLeft.Clear();
        buttonsRight.Clear();
    }

    public void DisableButtons()
    {
        
        DestroyButtons();
        buttonsEnabled = false;
    }

    private void runProblem(Problem prob)
    {
        currentProblem = prob;
        //Create molecule buttons, place inside a Left list and a Right list
        for (int i = 0; i < prob.leftSide.Count; i++)
        {
            buttonsLeft.Add(Instantiate(MolButton, equationCanvas.transform));
            buttonsLeft[i].GetComponent<MolButtonController>().molecule = prob.leftSide[i];
            buttonsLeft[i].transform.localPosition = new Vector2((prob.leftSide.Count - i)*(-960f)/(prob.leftSide.Count+1), -100f);
        }

        for (int i = 0; i < prob.rightSide.Count; i++)
        {
            buttonsRight.Add(Instantiate(MolButton, equationCanvas.transform));
            buttonsRight[i].GetComponent<MolButtonController>().molecule = prob.rightSide[i];
            buttonsRight[i].transform.localPosition = new Vector2((i+1)*(960f)/(prob.rightSide.Count+1), -100f);
        }

        for (int i = 0; i < buttonsLeft.Count; i++)
        {
            Navigation NewNavigation = new Navigation();
            NewNavigation.mode = Navigation.Mode.Explicit;
            NewNavigation.selectOnRight = 
                (i == buttonsLeft.Count-1) ? buttonsRight[0].GetComponent<Button>() : buttonsLeft[i+1].GetComponent<Button>();
            NewNavigation.selectOnLeft =
                (i == 0) ? buttonsRight[buttonsRight.Count-1].GetComponent<Button>() : buttonsLeft[i-1].GetComponent<Button>();
            buttonsLeft[i].GetComponent<Button>().navigation = NewNavigation;
        }

        for (int i = 0; i < buttonsRight.Count; i++)
        {
            Navigation NewNavigation = new Navigation();
            NewNavigation.mode = Navigation.Mode.Explicit;
            NewNavigation.selectOnRight = 
                (i == buttonsRight.Count-1) ? buttonsLeft[0].GetComponent<Button>() : buttonsRight[i+1].GetComponent<Button>();
            NewNavigation.selectOnLeft =
                (i == 0) ? buttonsLeft[buttonsLeft.Count-1].GetComponent<Button>() : buttonsRight[i-1].GetComponent<Button>();
            buttonsRight[i].GetComponent<Button>().navigation = NewNavigation;
        }

        EventSystem.current.SetSelectedGameObject(buttonsLeft[0]);
        currentSelection = EventSystem.current.currentSelectedGameObject;
    }
}
