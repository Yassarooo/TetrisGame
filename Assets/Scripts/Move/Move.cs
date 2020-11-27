using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour {
    public static bool ValidMove (Transform tr) {
        foreach (Transform children in tr) {
            Debug.Log ("Entered ValidMove()");
            Debug.Log (tr.position);
            int roundedX = Mathf.RoundToInt (children.transform.position.x);
            int roundedY = Mathf.RoundToInt (children.transform.position.y);
            int roundedZ = Mathf.RoundToInt (children.transform.position.z) - 1;

            if (roundedX < 0 || roundedX >= Spawner.instance.gridSizeX || roundedY < 0 || roundedY >= Spawner.instance.gridSizeY || roundedZ < 0 || roundedZ >= Spawner.instance.gridSizeZ) {
                //Game2DManager.instance.PlaySound (5);
                Debug.Log ("Valid Move returned        false1");
                return false;
            }
            if (Grid.grid[roundedX, roundedY, roundedZ] != null) {
                Debug.Log ("Valid Move returned        false2");
                return false;
            }

        }
        Debug.Log ("return true ");
        return true;
    }
}