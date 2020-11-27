using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : Move
{
    public MoveForward()
    {
        Block.instance.transform.position += Vector3.forward;
        if (!ValidMove(Block.instance.transform))
            Block.instance.transform.position += Vector3.back;
    }
}
