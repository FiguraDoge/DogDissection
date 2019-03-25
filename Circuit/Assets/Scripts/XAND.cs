using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAND : Gate
{
    public override bool truthTable(Basic[] input)
    {
        if (input[0] == null || input[1] == null)
            return false;

        if (input[0].getOutput() != input[1].getOutput())
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether input and connected gameobject has changed, therefore output
        // XAND need at least one power source
        if (scpt.input[0] == null && scpt.input[1] == null)
            scpt.setOutput(false);
        // Check the truth table for XAND
        else
            scpt.setOutput(truthTable(scpt.input));
    }
}
