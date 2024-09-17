using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MolButtonController : MonoBehaviour
{
    public Molecule molecule;

    private void Start()
    {
        gameObject.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex()-1);
        GetComponentInChildren<TMP_Text>().text = molecule.ToString();
    }

    public void IncrementMolecule()
    {
        molecule.Increment();
        GetComponentInChildren<TMP_Text>().text = molecule.ToString();
    }

    public void DecrementMolecule()
    {
        molecule.Decrement();
        GetComponentInChildren<TMP_Text>().text = molecule.ToString();
    }
}
