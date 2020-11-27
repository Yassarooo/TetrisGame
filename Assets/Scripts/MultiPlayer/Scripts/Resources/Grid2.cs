using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Grid2 : MonoBehaviour
{

    public static int width = 10;
    public static int height = 20;
    public int cleanRowCount = 0;
    public Transform[,] grid = new Transform[20, height];
    public Player2 pl;
    
    public Transform startpoint;
  /*  #region Singleton
    public static Grid instance;
    public Grid()
    {
        instance = this;
    }
    #endregion*/

    private void Awake()
    {
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
                grid[i, j] = null;
    }

    private void Update()
    {
        
    }

    public void CheckForLines()
    {
        int multiple = 0;

        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                multiple++;
                cleanRowCount++;

                DeleteLine(i);
                RowDown(i);
            }
        }

        if (multiple == 4)
            pl.score += (1200);
        else if (multiple == 3)
            pl.score += (300);
        else if (multiple == 2)
            pl.score += 100;
        else if (multiple == 1)
            pl.score += 40;
    }
    bool HasLine(int i)
    {
        
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    public void AddToGrid(Transform transform)
    {
        foreach (Transform children in transform)
        {
            try
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x) - Mathf.RoundToInt(startpoint.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y) - Mathf.RoundToInt(startpoint.transform.position.y);
                if (roundedY >= height)
                {
                    pl.disable();
                    return;
                }
                Debug.Log(roundedX + " " + roundedY);
                Transform _grid = grid[roundedX, roundedY];
                if (_grid == null)
                {
                    grid[roundedX, roundedY] = children;
                }
                else
                {
                    pl.disable();
                    return;
                }
            }
            catch (System.Exception ex)
            {
                Time.timeScale = 0;
                GamePlayManager.instance.gameOverPanel.SetActive(true);
                GamePlayManager.instance.FinalScore.text = PlayerPrefs.GetInt("point").ToString();
                Debug.LogError(ex.Message);
            }
        }
    }

}
