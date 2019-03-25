using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOT
 *        TruthTable
 * LeftInput    Output
 *     0           1
 *     1           0
 */

public class NOT : Gate
{
    private Basic leftInputScpt;
    private Basic outputScpt;

    void Start()
    {
        // No input and output by default
        // Only one input and one output
        rightInputPort = null;
        leftInputScpt = leftInputPort.GetComponent<Basic>();
        outputScpt = outputPort.GetComponent<Basic>();
    }

    void Update()
    {
        // Need at least one power source
        if (!leftInputScpt.getCurrent())
            outputScpt.setOutput(false);
        // Check the truth table
        else
            outputScpt.setOutput(truthTable(leftInputScpt.getOutput(), false));
        outputScpt.setCurrent(leftInputScpt.getCurrent());
    }

    public override bool truthTable(bool left, bool right)
    {
        return !left;
    }
}
