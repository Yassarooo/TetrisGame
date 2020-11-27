using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottonInputs : MonoBehaviour {
    public static bottonInputs instance;

    public GameObject[] rotateCanvases;
    public GameObject moveCanvas;

    GameObject activeObject;

    bool moveIsOn = true;

    void Awake () {
        instance = this;

    }

    void Start () {
        SetInputs ();
    }

    void RepositionToActiveBlock () {
        if (activeObject != null) {
            transform.position = activeObject.transform.position;
        }
    }
    void Update () {
        activeObject = Block.instance.gameObject;
        RepositionToActiveBlock ();
    }

    public void SwitchInputs () {
        moveIsOn = !moveIsOn;
        SetInputs ();
    }

    void SetInputs () {
        moveCanvas.SetActive (moveIsOn);
        foreach (GameObject c in rotateCanvases) {
            c.SetActive (!moveIsOn);
        }

    }

    public void SetHighSpeed () {
        Game3DManager.instance.highspeed = true;
    }
}