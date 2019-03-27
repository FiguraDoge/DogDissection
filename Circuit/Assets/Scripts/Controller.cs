using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEditor;
using System;
using System.IO;

public class Controller : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean deleteAction;

    public string select;
    private float checkCD;
    private float selectCD;
    private float buildCD;
    private float deletCD;
    private float rotateCD;


    private GameObject movObj;

    private bool CheckGrab()
    {
        return grabAction.GetState(handType);
    }

    private bool CheckDelete()
    {
        return deleteAction.GetState(handType);
    }

    void Start()
    {
        movObj = null;
        checkCD = 0f;
        selectCD = 0f;
        buildCD = 0f;
        deletCD = 0f;
        rotateCD = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Interact Check
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30, layerMask))
        {
            // If it hits the samples on the wall and player grabed it 
            // select = obj
            if (hit.transform.tag == "Sample" && CheckGrab() && checkCD <= 0)
            {
                if (selectCD <= 0)
                {
                    select = hit.transform.gameObject.name;
                    selectCD = 1f;
                }
                checkCD = 0.2f;
            }

            // If it hits the ground, which means player is going to place selected obj down
            // Interact button is still the same
            else if (hit.transform.tag == "Ground")
            {
                if (select != null && CheckGrab() && movObj == null && checkCD <= 0)
                {
                    if (buildCD <= 0)
                    {
                        movObj = Instantiate(Resources.Load(select, typeof(GameObject)), hit.point, Quaternion.identity) as GameObject;
                        movObj.tag = "Real";
                        buildCD = 3f;
                    }
                    checkCD = 0.2f;
                }
                else if (movObj != null)
                {
                    movObj.transform.position = hit.point;
                }  
            }
            else if (movObj != null && hit.transform.tag == movObj.tag)
            {
                if (CheckGrab() && checkCD <= 0)
                {
                    checkCD = 0.2f;
                    movObj = null;
                }
            }

            // If it hits an existing obj, which means player is either going to delete it or rotate it
            // Delete: trackpad     rotate: trigger
            else if (hit.transform.tag == "Real")
            {
                if (CheckDelete() && deletCD <= 0)
                {
                    Destroy(hit.transform.gameObject);
                    deletCD = 1f;
                }

                if (CheckGrab() && rotateCD <= 0 && checkCD <= 0 && movObj == null)
                {
                    hit.transform.Rotate(0, 90, 0);
                    rotateCD = 0.2f;
                    checkCD = 0.2f;
                }
            }

            checkCD -= Time.deltaTime;
            selectCD -= Time.deltaTime;
            buildCD -= Time.deltaTime;
            deletCD -= Time.deltaTime;
            rotateCD -= Time.deltaTime;
        }
    }
}
