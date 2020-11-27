using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : Move
{
    public MoveBack()
    {
        Block.instance.transform.position += Vector3.back;
        if (!ValidMove(Block.instance.transform))
            Block.instance.transform.position += Vector3.forward;
    }
}
