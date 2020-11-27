using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanRow : Move {
    public CleanRow () {
        for (int j = 0; j < Spawner.instance.gridSizeX; j++) {
            for (int z = 0; z < Spawner.instance.gridSizeZ; z++) {
                if (Grid.grid[j, PlayerPrefs.GetInt ("CleanRow"), z] != null) {
                    Destroy (Grid.grid[j, PlayerPrefs.GetInt ("CleanRow"), z].gameObject);
                    Grid.grid[j, PlayerPrefs.GetInt ("CleanRow"), z] = null;
                }

            }
        }
        this.RowDown ();
    }

    public void RowDown () {
        for (int y = 1; y < Spawner.instance.gridSizeY; y++) {
            for (int j = 0; j < Spawner.instance.gridSizeX; j++) {
                for (int z = 0; z < Spawner.instance.gridSizeZ; z++) {
                    if (Grid.grid[j, y, z] != null) {
                        Grid.grid[j, y - 1, z] = Grid.grid[j, y, z];
                        Grid.grid[j, y, z] = null;
                        Grid.grid[j, y - 1, z].transform.position -= new Vector3 (0, 1, 0);

                    }
                }
            }
        }
    }
}