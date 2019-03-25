using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Transmitter: the bridge between all the elements
 * The input is also output
 * If one of the input has true output, then others shall has the same value
 * I decided call them port since they can be both input and output, the only difference is left and right
 */ 

public class Transmitter : MonoBehaviour
{
    public GameObject leftPort;
    public GameObject rightPort;

    private Basic leftInputScpt;
    private Basic rightInputScpt;

    void Start()
    {
        leftInputScpt = leftPort.GetComponent<Basic>();
        rightInputScpt = rightPort.GetComponent<Basic>();

        leftInputScpt.setInput(new List<Basic>());
        leftInputScpt.setOutput(false);
        leftInputScpt.setCurrent(false);

        rightInputScpt.setInput(new List<Basic>());
        rightInputScpt.setOutput(false);
        rightInputScpt.setOutput(false);
    }

    void Update()
    {
        // leftPort and rightPort have to share the same value
        // If one of them is true, then the other should be true as well
        if (leftInputScpt.getOutput() || rightInputScpt.getOutput())
        {
            leftInputScpt.setOutput(true);
            rightInputScpt.setOutput(true);
        }

        if (leftInputScpt.getCurrent() || rightInputScpt.getCurrent())
        {
            leftInputScpt.setCurrent(true);
            rightInputScpt.setCurrent(true);
        }
    }
}
