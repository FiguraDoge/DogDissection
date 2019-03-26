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
    public GameObject inputPort;
    public GameObject outputPort;

    private Basic inputScpt;
    private Basic outputScpt;

    void Start()
    {
        inputScpt = inputPort.GetComponent<Basic>();
        outputScpt = outputPort.GetComponent<Basic>();
    }

    void Update()
    {
        outputScpt.setOutput(inputScpt.getOutput());
        outputScpt.setCurrent(inputScpt.getCurrent());
    }


}
