using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  This is the port for transmitter, or similiar unit
 *  The speciallity is that for this kind of unit, input is also output 
 *  and I do no wish they have infinite input connection
 *  So I will constrain the Count of input will always <= 1
 *  
 *  However, it still shares some functions with inputPort
 */
  
public class TransmitterPort : MonoBehaviour
{
    private float radius;       // The radius for connection detection
    private Basic scpt;         // The basic script attached to inpuPort object

    void Start()
    {
        radius = 0.05f;
        scpt = this.gameObject.GetComponent<Basic>();
        scpt.setInput(new List<Basic>());
        scpt.setOutput(false);
        scpt.setCurrent(false);
    }

    // Update is called once per frame
    void Update()
    {
        scpt.setInput(portDetection());
        // Different than inputPort, I only set output to true when it is true
        // Because false can be turned to ture, depending on the other end 
        // So does current
        if (portOutput(scpt.getInput()))
            scpt.setOutput(true);
        if (portCurrent(scpt.getInput()))
            scpt.setCurrent(true);
    }

    private List<Basic> portDetection()
    {
        // Find the cloest connection in the radius
        // Different than the inputPort, we only need the cloest one
        Basic scpt = null;
        float minDist = Mathf.Infinity;
        Collider[] hit = Physics.OverlapSphere(this.gameObject.transform.position, radius);
        for (int i = 0; i < hit.Length; i++)
        {
            GameObject obj = hit[i].gameObject;
            if (obj != this.gameObject && (obj.tag == "Input" || obj.tag == "Output"))
            {
                float dist = Vector3.Distance(obj.transform.position, this.gameObject.transform.position);
                if (dist < minDist)
                {
                    scpt = obj.GetComponent<Basic>();
                    minDist = dist;
                }
            }
        }

        return scpt != null ?  new List<Basic> { scpt } :  new List<Basic>();
    }

    // In reality, input should not consider output
    // But this will simplify the job for the unit it connects to
    private bool portOutput(List<Basic> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].getOutput())
                return true;
        }
        return false;
    }

    private bool portCurrent(List<Basic> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].getCurrent())
                return true;
        }
        return false;
    }
}
