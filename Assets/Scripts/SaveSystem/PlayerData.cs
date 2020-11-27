using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<int> items = new List<int>();
    public int coins;
    public PlayerData()
    {
        for (int i = 0; i < Player.item.Count; i++)
            this.items.Add(Player.item[i]);

        this.coins = Player.coin;
    }
}
