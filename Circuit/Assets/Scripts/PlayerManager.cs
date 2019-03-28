using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public string select;
    public float checkCD;
    public float selectCD;
    public float buildCD;
    public float deletCD;
    public float rotateCD;
    public GameObject movObj;

    void Start()
    {
        select = null;
        movObj = null;
        checkCD = 0f;
        selectCD = 0f;
        buildCD = 0f;
        deletCD = 0f;
        rotateCD = 0f;
    }

}
