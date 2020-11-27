using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFactory : MonoBehaviour {

    public Move PerformMove (string type) {
        if (type == "Left") {
            return new MoveLeft ();
        } else if (type == "Right") {
            return new MoveRight ();
        } else if (type == "Down") {
            return new MoveDown ();
        } else if (type == "CleanRow") {
            Debug.LogError("CleanRow");
            return new CleanRow ();
        }else if (type == "Bomb") {
            Debug.LogError("Boooomb");
            return new CleanGrid ();
        } else if (type == "Forward") {
            return new MoveForward ();
        } else if (type == "Back") {
            return new MoveBack ();
        } else if (type == "PosX" || type == "NegX" || type == "PosY" || type == "NegY" || type == "PosZ" || type == "NegZ" || type == "Up") {
            return new Rotate3D (type);
        }
        return null;
    }
}