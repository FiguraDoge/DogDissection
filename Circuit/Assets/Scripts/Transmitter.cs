using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    Basic scpt; 
    public float radius;                                // The radius for connection detection
    public Transform[] childTS;                         // 0: SELF, 1: LEFT, 2: RIGHT

    void Start()
    {
        scpt = this.gameObject.GetComponent<Basic>();
        scpt.setInput(new Basic[2] { null, null });    
        scpt.setOutput(false);
        childTS = gameObject.GetComponentsInChildren<Transform>();
        childTS = new Transform[2] { childTS[1], childTS[2] };
    }


    void Update()
    {
        // 1. Detect the connection
        GameObject[] newConnectInput = new GameObject[2] { null, null };

        // Find the closest object in the raduis of left end and add it into connect
        float minDist = Mathf.Infinity;
        Collider[] left_hit = Physics.OverlapSphere(childTS[0].position, radius);
        for (int i = 0; i < left_hit.Length; i++)
        {
            GameObject obj = left_hit[i].gameObject;
            if (obj != this.gameObject && (obj.tag == "Input" || obj.tag == "Output"))
            {
                float dist = Vector3.Distance(obj.transform.position, childTS[1].position);
                if (dist < minDist)
                {
                    newConnectInput[0] = obj;
                    minDist = dist;
                }
            }
        }

        // Find the closest object in the raduis of right end and add it into connect
        minDist = Mathf.Infinity;
        Collider[] right_hit = Physics.OverlapSphere(childTS[1].position, radius);
        for (int i = 0; i < right_hit.Length; i++)
        {
            GameObject obj = right_hit[i].gameObject;
            if (obj != this.gameObject && (obj.tag == "Input" || obj.tag == "Output"))
            {
                float dist = Vector3.Distance(obj.transform.position, childTS[1].position);
                if (dist < minDist)
                {
                    newConnectInput[1] = obj;
                    minDist = dist;
                }
            }
        }

        // 2. If the conntected object changed, 
        //    and can be connected to the new object, newinput.input != this
        //    modify the new connected object.input
        //    modify the orignal connected object.input

        int idx_1;
        int idx_2;
        Basic newInputScpt_1 = null;
        Basic newInputScpt_2 = null;

        if (newConnectInput[0] != null)
        {
            idx_1 = newConnectInput[0].transform.GetSiblingIndex();       // the corresponding index in the basic
            newInputScpt_1 = newConnectInput[0].transform.parent.gameObject.GetComponent<Basic>();
            // Enough spot or it is a power source
            // Set empty spot to this basic, vice verce
            if (newConnectInput[0].transform.parent.gameObject.tag == "Power")
                scpt.changeInput(newInputScpt_1, 0);
            else if (newInputScpt_1.getInput(idx_1) == null || newInputScpt_1.getInput(idx_1) == this)
            {
                // If remain the same, no need to delete
                // Else delete the orignal one connection
                newInputScpt_1.changeInput(scpt, idx_1);
                scpt.changeInput(newInputScpt_1, 0);
            }
        }
        else
        {
            // change the origonal to null, if it has one
            scpt.changeInput(null, 0);
        }
        


        if (newConnectInput[1] != null)
        {
            idx_2 = newConnectInput[1].transform.GetSiblingIndex();       // the corresponding index in the basic
            newInputScpt_2 = newConnectInput[1].transform.parent.gameObject.GetComponent<Basic>();
            if (newConnectInput[1].transform.parent.gameObject.tag == "Power")
                scpt.changeInput(newInputScpt_2, 1);
            else if (newInputScpt_2.getInput(idx_2) == null || newInputScpt_2.getInput(idx_2) == null)
            {
                newInputScpt_2.changeInput(scpt, idx_2);
                scpt.changeInput(newInputScpt_2, 1);
            }
        }
        else
        {
            scpt.changeInput(null, 1);
        }
            

        // 3. Change output
        //    Output is true when either of connects has a true output
        Basic input_1 = scpt.getInput(0);
        Basic input_2 = scpt.getInput(1);

        if (input_1 == null && input_2 == null)
            scpt.setOutput(false);
        else if (input_1 == null)
            scpt.setOutput(input_2.getOutput());
        else if (input_2 == null)
            scpt.setOutput(input_1.getOutput());
        else
            scpt.setOutput((input_1.getOutput() || input_2.getOutput()));
    }
}
