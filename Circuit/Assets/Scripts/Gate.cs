using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The parent script for all unit gate
public class Gate : MonoBehaviour
{
    public Basic scpt;

    // Different gates have different table
    public virtual bool truthTable(Basic[] input)
    {
        return false;
    }

    void Start()
    {
        // no input and output by default
        scpt = this.gameObject.GetComponent<Basic>();
        scpt.setInput(new Basic[2] { null, null });      // input 1, input 2
        scpt.setOutput(false);
    }
}
