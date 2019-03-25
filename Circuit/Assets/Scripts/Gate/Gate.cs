using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gate: the parent script for all unit gate
 * All gates have two input ports, and one output port, except NOT
 * The input port can have infinite inputs
 * So does output port, but I only care the bool value of output instead of its connections
 * 
 * Different gates have different truthtable, so I use a virtual function here
 */ 

public class Gate : MonoBehaviour
{
    public GameObject leftInputPort;
    public GameObject rightInputPort;
    public GameObject outputPort;

    // Different gates have different table
    public virtual bool truthTable(bool left, bool right)
    {
        Debug.Log("Using virual function");
        return false;
    }
}
