using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCreate : NetworkBehaviour
{
    [SyncVar]
    public int score;
    [SyncVar]
    public bool end;

    private void Awake()
    {
        
    }

    public void endgame()
    {
        cmdSetnum();
    }

    public void set(int n)
    {
        cmdSetscore(n);
    }


    [Command]
    void cmdSetnum()
    {
        RpcSetnum();
    }

    [ClientRpc]
    void RpcSetnum()
    {
        end = true;
    }

    [Command]
    void cmdSetscore(int n)
    {
        RpcSetscore(n);
    }

    [ClientRpc]
    void RpcSetscore(int n)
    {
        score = n;
    }


}
