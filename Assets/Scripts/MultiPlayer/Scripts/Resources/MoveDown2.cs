using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveDown2 : Move2
{
    public MoveDown2(Grid2 gd, Block2 bc, Spawner2 sp, float _fallTime)
    {
        this.gd = gd;

        if (Time.time - bc.previosTime > _fallTime ||  bc.previosTime -Time.time > _fallTime)
        {
        //repeat:
            bc.transform.position += new Vector3(0, -1, 0);
            Debug.Log("carol");
            if (!ValidMove(bc.transform))
            {
                bc.transform.position -= new Vector3(0, -1, 0);
                gd.AddToGrid(bc.transform);
                gd.CheckForLines();
                bc.enabled = false;
                sp.CreateNewBlock(true);

                if (Time.timeScale == 0)
                    return;
                GamePlayManager.instance.PlaySound(3);
            }

            bc.previosTime = Time.time;
        }
    }
}
