using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MolButtonController : MonoBehaviour
{
    [SerializeField] private AudioSource AudioIncrement;
    [SerializeField] private AudioSource AudioDecrement;
    public Molecule molecule;
    Animator buttonAnimator;

    private void Start()
    {
        gameObject.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex()-1);
        transform.Find("MoleculeText").GetComponent<TMP_Text>().text = molecule.ToString();
        transform.Find("AmountText").GetComponent<TMP_Text>().text = "";
        buttonAnimator = GetComponent<Animator>();
    }

    public void IncrementMolecule()
    {
        molecule.Increment();
        transform.Find("AmountText").GetComponent<TMP_Text>().text = molecule.amount.ToString();
        buttonAnimator.Play("increment", -1, 0f);
        AudioIncrement.Play();

    }

    public void DecrementMolecule()
    {
        molecule.Decrement();
        transform.Find("AmountText").GetComponent<TMP_Text>().text = (molecule.amount == 1) ? "" : molecule.amount.ToString();
        AudioDecrement.Play();
    }
}
