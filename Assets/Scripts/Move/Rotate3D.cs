using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Rotate3D : Move {
    public Rotate3D (string direction) {
        if (Block.instance.gameObject != null) {
            if (direction == "PosX") {
                SetRotationInput (new Vector3 (90, 0, 0));
            }
            if (direction == "NegX") {
                SetRotationInput (new Vector3 (-90, 0, 0));
            }
            if (direction == "PosY") {
                SetRotationInput (new Vector3 (0, 90, 0));
            }
            if (direction == "NegY") {
                SetRotationInput (new Vector3 (0, -90, 0));
            }
            if (direction == "PosZ" ||direction == "Up") {
                SetRotationInput (new Vector3 (0, 0, 90));
            }
            if (direction == "NegZ") {
                SetRotationInput (new Vector3 (0, 0, -90));
            }
        }
    }

    public void SetRotationInput (Vector3 rotation) {
        Block.instance.transform.Rotate (rotation, Space.World);
        if (!ValidMove (Block.instance.transform)) {
            Block.instance.transform.Rotate (-rotation, Space.World);
        }
    }
}