using System;
using UnityEngine;
using Mirror;

public class Block2 : NetworkBehaviour
{
    public static Vector3 rotationPoint;
    public float previosTime = 0;
    public static float fallTime = 1f;
    public static float fallCrosser = 1f;
    public static bool gameRunningStatus = true;
    public bool on;

    /*  private static bool leftActive = false;
     private static bool rightActive = false;
     private static bool downActive = false;
     private static bool upActive = false; */


    /*
    #region Singleton
    public static Block instance;
    public Block()
    {
        instance = this;
    }
    #endregion
    */
    public Block2()
    {
        on = true;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("level", 1) == 1)
            fallTime = 1f;
        else if (PlayerPrefs.GetInt("level", 1) == 2)
            fallTime = .8f;
        else if (PlayerPrefs.GetInt("level", 1) == 3)
            fallTime = .6f;
        else if (PlayerPrefs.GetInt("level", 1) == 4)
            fallTime = .5f;
        else if (PlayerPrefs.GetInt("level", 1) == 5)
            fallTime = .4f;
        else if (PlayerPrefs.GetInt("level", 1) == 6)
            fallTime = .3f;
        else if (PlayerPrefs.GetInt("level", 1) == 7)
            fallTime = .2f;
        else
            fallTime = .1f;

        if (!gameRunningStatus && Time.timeScale == 0)
            return;




    }

    public void GoDown()
    {
        
    }

    public void GoUp()
    {
        
    }


    /* public void LeftClick()
    {
        leftActive = true;
        GamePlayManager.instance.PlaySound(4);
    }
    public void RightClick()
    {
        rightActive = true;
        GamePlayManager.instance.PlaySound(4);
    }
    public void UpClick()
    {
        upActive = true;
        GamePlayManager.instance.PlaySound(4);
    }
    public void DownClick()
    {
        downActive = true;
        GamePlayManager.instance.PlaySound(4);
    } */




}