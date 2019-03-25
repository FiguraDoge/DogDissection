using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    void Start()
    {
        Basic scpt = gameObject.GetComponent<Basic>();
        scpt.setOutput(true);
    }
}
