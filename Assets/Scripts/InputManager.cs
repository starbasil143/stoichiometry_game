using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public bool MoleculeIncrementInput { get; private set; }
    public bool MoleculeDecrementInput { get; private set; }
    public bool CheckAnswerInput { get; private set; }

    private PlayerInput playerInput;

    private InputAction incrementAction;
    private InputAction decrementAction;
    private InputAction checkAnswerAction;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();
        incrementAction = playerInput.actions["MoleculeIncrement"];
        decrementAction = playerInput.actions["MoleculeDecrement"];
        checkAnswerAction = playerInput.actions["CheckAnswer"];
    }
    
    private void Update()
    {
        MoleculeIncrementInput = incrementAction.WasPressedThisFrame();
        MoleculeDecrementInput = decrementAction.WasPressedThisFrame();
        CheckAnswerInput = checkAnswerAction.WasPressedThisFrame();
    }

}
