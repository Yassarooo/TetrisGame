﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveLeft2 : Move2
{
    public MoveLeft2(Grid2 gd,Block2 bc)
    {
        this.gd = gd;
        bc.transform.position += new Vector3(1, 0, 0);
        if (!ValidMove(bc.transform))
            bc.transform.position -= new Vector3(1, 0, 0);
    }
}
