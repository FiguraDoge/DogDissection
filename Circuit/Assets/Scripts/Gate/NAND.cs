using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NAND
 *              TruthTable
 * LeftInput    RightInput      Output
 *     0            0              1
 *     0            1              1
 *     1            0              1
 *     1            1              0
 */
  
public class NAND : Gate
{
    private Basic leftInputScpt;
    private Basic rightInputScpt;
    private Basic outputScpt;

    void Start()
    {
        // No input and output by default
        leftInputScpt = leftInputPort.GetComponent<Basic>();
        rightInputScpt = rightInputPort.GetComponent<Basic>();
        outputScpt = outputPort.GetComponent<Basic>();
    }

    void Update()
    {
        // Need at least one power source
        if (!leftInputScpt.getCurrent() && !rightInputScpt.getCurrent())
            outputScpt.setOutput(false);
        // Check the truth table
        else
            outputScpt.setOutput(truthTable(leftInputScpt.getOutput(), rightInputScpt.getOutput()));
        outputScpt.setCurrent(leftInputScpt.getCurrent() || rightInputScpt.getCurrent());
    }

    public override bool truthTable(bool left, bool right)
    {
        if (left && right)
            return false;
        else
            return true;
    }
}
