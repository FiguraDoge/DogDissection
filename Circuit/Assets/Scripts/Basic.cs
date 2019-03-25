using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{
    public Basic[] input;
    public bool output;

    public void setOutput(bool value)
    {
        output = value;
    }

    public void setInput(Basic[] value)
    {
        input = value;
    }

    public void changeInput(Basic value, int idx)
    {
        input[idx] = value;
    }

    public bool getOutput()
    {
        return output;
    }

    public Basic[] getInput()
    {
        return input;
    }

    public Basic getInput(int idx)
    {
        if (idx >= 0 && idx <= 2) 
            return input[idx];
        return null;
    }
}
