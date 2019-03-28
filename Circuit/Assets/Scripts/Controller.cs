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

    private PlayerManager playerManager;

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
        playerManager = PlayerManager.instance;
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
            if (hit.transform.tag == "Sample" && CheckGrab() && playerManager.checkCD <= 0)
            {
                if (playerManager.selectCD <= 0)
                {
                    playerManager.select = hit.transform.gameObject.name;
                    playerManager.selectCD = 1f;
                }
                playerManager.checkCD = 0.4f;
            }

            // If it hits the ground, which means player is going to place selected obj down
            // Interact button is still the same
            else if (hit.transform.tag == "Ground")
            {
                if (playerManager.select != null && CheckGrab() && playerManager.movObj == null && playerManager.checkCD <= 0)
                {
                    if (playerManager.buildCD <= 0)
                    {
                        playerManager.movObj = Instantiate(Resources.Load(playerManager.select, typeof(GameObject)), hit.point, Quaternion.identity) as GameObject;
                        playerManager.movObj.tag = "Real";
                        playerManager.buildCD = 2f;
                    }
                    playerManager.checkCD = 0.4f;
                }
                else if (playerManager.movObj != null)
                {
                    playerManager.movObj.transform.position = hit.point;
                }  
            }
            else if (playerManager.movObj != null && hit.transform.tag == playerManager.movObj.tag)
            {
                if (CheckGrab() && playerManager.checkCD <= 0)
                {
                    playerManager.checkCD = 0.4f;
                    playerManager.movObj = null;
                }
                else
                {
                    playerManager.movObj.transform.position = new Vector3(hit.point.x, 0, hit.point.z); ;
                }
            }

            // If it hits an existing obj, which means player is either going to delete it or rotate it
            // Delete: trackpad     rotate: trigger
            else if (hit.transform.tag == "Real")
            {
                if (CheckDelete() && playerManager.deletCD <= 0)
                {
                    Destroy(hit.transform.gameObject);
                    playerManager.deletCD = 1f;
                }

                if (CheckGrab() && playerManager.rotateCD <= 0 && playerManager.checkCD <= 0 && playerManager.movObj == null)
                {
                    hit.transform.Rotate(0, 90, 0);
                    playerManager.rotateCD = 0.4f;
                    playerManager.checkCD = 0.4f;
                }
            }

            playerManager.checkCD -= Time.deltaTime;
            playerManager.selectCD -= Time.deltaTime;
            playerManager.buildCD -= Time.deltaTime;
            playerManager.deletCD -= Time.deltaTime;
            playerManager.rotateCD -= Time.deltaTime;
        }
    }
}
