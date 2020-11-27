using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    [System.Serializable]
    public class index {
        public int x, y, z;
        public index () {
            x = -1;
            y = -1;
            z = -1;
        }
    }

    [System.Serializable]
    public class posi {
        public float x, y, z;
        public posi () {
            x = -1000;
            y = -1000;
            z = -1000;
        }
    }
    public index[, , ] idxs = new index[Spawner.instance.gridSizeX, Spawner.instance.gridSizeY, Spawner.instance.gridSizeZ];

    public posi[, , ] posis = new posi[Spawner.instance.gridSizeX, Spawner.instance.gridSizeY, Spawner.instance.gridSizeZ];
    public string[, , ] names;
    public GameData () {
        this.names = new string[Spawner.instance.gridSizeX, Spawner.instance.gridSizeY, Spawner.instance.gridSizeZ];

        for (int y = 0; y < Spawner.instance.gridSizeY; y++) {
            for (int j = 0; j < Spawner.instance.gridSizeX; j++) {
                for (int z = 0; z < Spawner.instance.gridSizeZ; z++) {
                    if (idxs[j, y, z] == null) {
                        idxs[j, y, z] = new index ();
                    }

                    if (posis[j, y, z] == null) {
                        posis[j, y, z] = new posi ();
                    }

                    if (Grid.grid[j, y, z] != null) {
                        idxs[j, y, z].x = j;
                        idxs[j, y, z].y = y;
                        idxs[j, y, z].z = z;
                        posis[j, y, z].x = Grid.grid[j, y, z].position.x;
                        posis[j, y, z].y = Grid.grid[j, y, z].position.y;
                        posis[j, y, z].z = Grid.grid[j, y, z].position.z;
                        names[j, y, z] = Grid.grid[j, y, z].name;
                    }
                }
            }
        }
        //this.activeBlock = Block.instance.transform.gameObject;
    }
}