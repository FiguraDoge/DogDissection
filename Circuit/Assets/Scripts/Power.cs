using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Power Source
 * Power Source has no input, the input is just itself
 * Power Source always has a true ouput
 */
  
public class Power : MonoBehaviour
{
    public GameObject outputPort;

    void Start()
    {
        Basic scpt = outputPort.GetComponent<Basic>();
        scpt.setInput(new List<Basic>() { scpt });
        scpt.setOutput(true);
        scpt.setCurrent(true);
    }
}
