using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveFactory2 : MonoBehaviour
{
    /* #region Singleton
    public static MoveFactory instance;

    public MoveFactory () {
        instance = this;
    }
    #endregion */

    public  Move2 PerformMove(string type,Block2 bc,Grid2 gd,Spawner2 sp,float fall = 0)
    {
        if (type == "Left")
        {
            return new MoveLeft2(gd,bc);
        }
        else if (type == "Right")
        {
            return new MoveRight2(gd,bc);
        }
        else if (type == "Up")
        {
            return new MoveUp2(gd,bc);
        }
        else if (type == "Down")
        {
            if (gd == null)
                Debug.Log("philip");
            return new MoveDown2(gd, bc, sp,fall);
        }
        return null;
    }
}