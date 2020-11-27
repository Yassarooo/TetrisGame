using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveUp2 : Move2
{
    public MoveUp2(Grid2 gd,Block2 bc)
    {
        this.gd = gd;
        bc.transform.RotateAround(bc.transform.TransformPoint(Block.rotationPoint), new Vector3(0, 0, 1), 90);
        if (!ValidMove(bc.transform))
           bc.transform.RotateAround(bc.transform.TransformPoint(Block.rotationPoint), new Vector3(0, 0, 1), -90);
    }
}
