using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Move2 : MonoBehaviour
{
    public Grid2 gd;

    public Move2(Grid2 o)
    {
        gd = o;
    }

    public Move2()
    {

    }
    public bool ValidMove (Transform transform) {
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x) - Mathf.RoundToInt(gd.startpoint.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y) - Mathf.RoundToInt(gd.startpoint.transform.position.y);
            
            if (roundedY < 0 || roundedX < 0 || roundedX >= Grid2.width) 
            {
                GamePlayManager.instance.PlaySound (5);
                return false;
            }

            if (roundedY<Grid2.height && gd.grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }
}
