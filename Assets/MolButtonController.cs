using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MolButtonController : MonoBehaviour
{
    public Molecule molecule;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TMP_Text>().text = molecule.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
