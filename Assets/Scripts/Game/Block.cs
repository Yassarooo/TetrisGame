using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static Vector3 rotationPoint;
    public static float previosTime=0;
    public static bool gameRunningStatus = true;
    public float fallTime = 1f;


    #region Singleton
    public static Block instance;
    public Block()
    {
        instance = this;
    }
    #endregion
    

}