using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour {

    public static int width;
    public static int cleanRowCount = 0;
    public static Transform[, , ] grid;
    #region Singleton
    public static Grid instance;
    public Grid () {
        instance = this;
    }
    #endregion
    public static void CheckForLines () {
        int multiple = 0;

        for (int i = Spawner.instance.gridSizeY - 1; i >= 0; i--) {
            if (HasLine (i)) {
                multiple++;
                cleanRowCount++;
                PlayerPrefs.SetInt ("lines", cleanRowCount);
                int _level = ((cleanRowCount / 10) == 0 ? 1 : cleanRowCount / 10);
                if (cleanRowCount > 10) {
                    int _level_ = _level + 1;
                    PlayerPrefs.SetInt ("level", _level_);
                }

                DeleteLine (i);
                RowDown (i);
            }
        }
        if (multiple > 0) {
            if (SceneManager.GetActiveScene ().buildIndex == 2) {
                Game2DManager.instance.SetScore (multiple);
            }
            if (SceneManager.GetActiveScene ().buildIndex == 3) {
                Game3DManager.instance.SetScore (multiple);
            }
        }

    }
    static bool HasLine (int i) {
        for (int j = 0; j < width; j++) {
            for (int z = 0; z < Spawner.instance.gridSizeZ; z++) {
                if (grid[j, i, z] == null)
                    return false;
            }
        }
        return true;
    }
    static void DeleteLine (int i) {
        for (int j = 0; j < width; j++) {
            for (int z = 0; z < Spawner.instance.gridSizeZ; z++) {

                Destroy (grid[j, i, z].gameObject);
                grid[j, i, z] = null;
            }
        }
    }
    public static void RowDown (int i) {
        for (int y = i; y < Spawner.instance.gridSizeY; y++) {
            for (int j = 0; j < width; j++) {
                for (int z = 0; z < Spawner.instance.gridSizeZ; z++) {
                    if (grid[j, y, z] != null) {
                        grid[j, y - 1, z] = grid[j, y, z];
                        grid[j, y, z] = null;
                        grid[j, y - 1, z].transform.position -= new Vector3 (0, 1, 0);

                    }
                }
            }
        }
    }
    public static void AddToGrid (Transform transform) {
        foreach (Transform children in transform) {
            try {
                int roundedX = Mathf.RoundToInt (children.transform.position.x);
                int roundedY = Mathf.RoundToInt (children.transform.position.y);
                int roundedZ = Mathf.RoundToInt (children.transform.position.z) - 1;
                Debug.Log ("roundedZ in ADDTOGRID() " + roundedZ);
                Debug.Log ("grid[roundedX, roundedY, roundedZ] in ADDTOGRID()" + grid[roundedX, roundedY, roundedZ]);
                Transform _grid = grid[roundedX, roundedY, roundedZ];
                if (_grid == null) {
                    grid[roundedX, roundedY, roundedZ] = children;
                    if (SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        Game2DManager.instance.PlaySound(3);
                        }
                    if (SceneManager.GetActiveScene().buildIndex == 3)
                    {
                        Game3DManager.instance.PlaySound(3);
                        }
                } else {
                    Time.timeScale = 0;
                    if (SceneManager.GetActiveScene ().buildIndex == 2) {
                        Game2DManager.instance.gameOverPanel.SetActive (true);
                        Game2DManager.instance.FinalScore.text = PlayerPrefs.GetInt ("point").ToString ();
                    }
                    if (SceneManager.GetActiveScene ().buildIndex == 3) {
                        Game3DManager.instance.gameOverPanel.SetActive (true);
                        Game3DManager.instance.FinalScore.text = PlayerPrefs.GetInt ("point").ToString ();
                    }

                    if (PlayerPrefs.GetInt ("HiScore", 0) < PlayerPrefs.GetInt ("point")) {
                        PlayerPrefs.SetInt ("HiScore", PlayerPrefs.GetInt ("point"));
                    }
                    Debug.Log ("GameOver");
                    break;
                }
            } catch (System.Exception ex) {
                Time.timeScale = 0;
                if (SceneManager.GetActiveScene ().buildIndex == 2) {
                    Game2DManager.instance.gameOverPanel.SetActive (true);
                    Game2DManager.instance.FinalScore.text = PlayerPrefs.GetInt ("point").ToString ();
                }
                if (SceneManager.GetActiveScene ().buildIndex == 3) {
                    Game3DManager.instance.gameOverPanel.SetActive (true);
                    Game3DManager.instance.FinalScore.text = PlayerPrefs.GetInt ("point").ToString ();
                }

                if (PlayerPrefs.GetInt ("HiScore", 0) < PlayerPrefs.GetInt ("point")) {
                    PlayerPrefs.SetInt ("HiScore", PlayerPrefs.GetInt ("point"));
                }
                Debug.LogError (ex.Message);
            }
        }
    }

}