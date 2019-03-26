using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{

    public GameObject inputPort;
    public GameObject outputPort;
    public GameObject bulb;
    
    private Basic inputPortScpt;
    private Basic outputPortScpt;
    private Renderer bulbRender;

    void Start()
    {
        inputPortScpt = inputPort.GetComponent<Basic>();
        outputPortScpt = outputPort.GetComponent<Basic>();

        bulbRender = bulb.GetComponent<Renderer>();
    }

    void Update()
    {
        if (inputPortScpt.getOutput())
        {
            outputPortScpt.setOutput(true);
            bulbRender.material.SetColor("Color_B96F998B", Color.yellow);
        }
        else
        {
            outputPortScpt.setOutput(false);
            bulbRender.material.SetColor("Color_B96F998B", Color.white);
        }   
    }

}
