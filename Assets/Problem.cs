using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Dictionary<String, int> GetElements()
    {
        Dictionary<String, int> totals = new Dictionary<String, int>();
        foreach ((String eName, int eAmount) element in elements)
        {
            totals.Add(element.eName, element.eAmount * amount);
        }
        return totals;
    }

    public override string ToString()
    {
        string output = (amount>1 ? "("+amount+")" : "");
        foreach((String eName, int eAmount) element in elements)
        {
            output += element.eName + ((element.eAmount>1) ? element.eAmount : "");
        }
        return output;
    }
}

public class Problem
{
    public List<Molecule> leftSide = new List<Molecule>();
    public List<Molecule> rightSide = new List<Molecule>();

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

    public int GetEquationSize()
    {
        return leftSide.Count + rightSide.Count;
    }

    public bool isBalanced()
    {
        Dictionary<String, int> leftElements = new Dictionary<String, int>();
        Dictionary<String, int> rightElements = new Dictionary<String, int>();
        foreach (Molecule mol in leftSide)
        {
            foreach (KeyValuePair<string, int> elem in mol.GetElements())
            {
                if(leftElements.ContainsKey(elem.Key))
                {
                    leftElements[elem.Key] += elem.Value;
                }
                else
                {
                    leftElements.Add(elem.Key, elem.Value);
                }
            }
        }
        foreach (Molecule mol in rightSide)
        {
            foreach (KeyValuePair<string, int> elem in mol.GetElements())
            {
                if(rightElements.ContainsKey(elem.Key))
                {
                    rightElements[elem.Key] += elem.Value;
                }
                else
                {
                    rightElements.Add(elem.Key, elem.Value);
                }
            }
        }
        if(leftElements.Count != rightElements.Count)
        {
            Debug.Log("incongruent counts");
            return false;
        }
        foreach (KeyValuePair<string, int> item in leftElements)
        {
            if(rightElements.ContainsKey(item.Key))
            {
                if(rightElements[item.Key] != item.Value)
                {
                    return false;
                }
                else
                {
                    //continue
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    

}