using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanGrid : Move
{
    public CleanGrid()
    {
        for (int j = 0; j < Spawner.instance.gridSizeX; j++)
        {
            for (int y = 0; y < Spawner.instance.gridSizeY; y++)
            {
                for (int z = 0; z < Spawner.instance.gridSizeZ; z++)
                {
                    Debug.LogError(j + "" + y + "" + z);
                    if (Grid.grid[j, y, z] != null)
                    {
                        Destroy(Grid.grid[j, y, z].gameObject);
                        Grid.grid[j, y, z] = null;
                    }
                }
            }
        }
    }
}