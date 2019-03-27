using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Common inputPort for most unit
 * Allow infinite connection 
 * 
 */
  
public class InputPort : MonoBehaviour
{
    private float radius;       // The radius for connection detection
    private Basic scpt;         // The basic script attached to inpuPort object

    // Start is called before the first frame update
    void Start()
    {
        radius = 0.1f;
        scpt = this.gameObject.GetComponent<Basic>();
        scpt.setInput(new List<Basic>());
        scpt.setOutput(false);
        scpt.setCurrent(false);
    }

    // Update is called once per frame
    void Update()
    {
        scpt.setInput(portDetection());
        scpt.setOutput(portOutput(scpt.getInput()));
        scpt.setCurrent(portCurrent(scpt.getInput()));
    }

    private List<Basic> portDetection()
    {
        // Find all the connection input(output) ports in the radius
        List<Basic> newInput = new List<Basic>();

        Collider[] hit = Physics.OverlapSphere(this.gameObject.transform.position, radius);
        for (int i = 0; i < hit.Length; i++)
        {
            GameObject obj = hit[i].gameObject;
            if (obj != this.gameObject && (obj.tag == "Input" || obj.tag == "Output"))
            {
                newInput.Add(obj.GetComponent<Basic>());
            }
        }
        return newInput;
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
