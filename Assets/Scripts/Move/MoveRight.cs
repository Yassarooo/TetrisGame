using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : Move
{
    public MoveRight()
    {
        Block.instance.transform.position += new Vector3(-1, 0, 0);
        if (!ValidMove(Block.instance.transform))
            Block.instance.transform.position -= new Vector3(-1, 0, 0);
    }
}
