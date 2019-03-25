using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{
    private List<Basic> input;       // The inputs
    private bool output;             // The output value 
    private bool current;            // Whether has a current flow

    public void setCurrent(bool value)
    {
        current = value;
    }

    public bool getCurrent()
    {
        return current;
    }

    public void setOutput(bool value)
    {
        output = value;
    }

    public bool getOutput()
    {
        return output;
    }

    public void setInput(List<Basic> value)
    {
        input = value;
    }

    public void setInput(Basic value, int idx)
    {
        input[idx] = value;
    }

    public List<Basic> getInput()
    {
        return input;
    }

    public Basic getInput(int idx)
    {
        if (idx >= 0 && idx < input.Count) 
            return input[idx];
        return null;
    }
}
