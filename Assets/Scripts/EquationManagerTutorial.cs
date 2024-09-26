using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquationManagerTutorial : MonoBehaviour
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



    public GameObject TutorialTextGroup1;
    
    public GameObject TutorialTextGroup2;
    
    public GameObject TutorialTextGroup3;
    

    private int[] problemSetAmounts = {3,3,3};
    public int maxScore;

    public int remainingLives = 4;
    public int score = 0;
    public int fails = 0;


    private GameObject currentSelection;

    private Problem currentProblem;

    
    private List<GameObject> buttonsLeft = new List<GameObject>();
    private List<GameObject> buttonsRight = new List<GameObject>();

    private int currentProblemNumber = 0;
    private bool buttonsEnabled;
    private int currentLevel;
    
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

    private void Start()
    {
        maxScore = 300;
        SceneManagementController = GameObject.FindWithTag("SceneManagementController");
        buttonsEnabled = true;
        SwitchText();
        runProblem(problemsTutorial[0]);
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
                if(currentProblem.isBalanced())
                {
                    if(currentProblemNumber < 2)
                    {
                        WinScreen.GetComponent<WinScreenController>().PlayLevelSwitchAnimation();
                        score+=100;
                    }
                    else
                    {
                        score+=100;
                        ResultsScreen.SetActive(true);
                    }
                }
                else
                {
                    if(currentProblemNumber == 2)
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
                    else
                    {
                        ErrorFlash.SetActive(false);
                        ErrorFlash.SetActive(true);
                    }
                }
            }
        }
    }

    public void RunNextProblem()
    {
        DestroyButtons();
        runProblem(problemsTutorial[++currentProblemNumber]);
        SwitchText();
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
    public void SwitchText()
    {
        switch (currentProblemNumber)
        {
            case 0: 
                TutorialTextGroup1.SetActive(true);
            break;
            
            case 1:
                TutorialTextGroup1.SetActive(false);
                TutorialTextGroup2.SetActive(true);
            break;

            case 2:
                TutorialTextGroup2.SetActive(false);
                TutorialTextGroup3.SetActive(true);
                Failsafe.transform.parent.gameObject.SetActive(true);
            break;
        }
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
