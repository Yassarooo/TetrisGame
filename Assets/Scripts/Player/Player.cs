using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player : MonoBehaviour
{

    public static int cntr = 0;
    public MoveFactory mf;
    public Move move;
    public static List<int> item = new List<int>();
    public static int coin = 500;

    public void CheckInput()
    {
        Debug.Log("Entered CheckInput()");
        move = mf.PerformMove("Down");
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && Time.timeScale != 0)
        {
            move = mf.PerformMove("Left");
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow)) && Time.timeScale != 0)
        {
            move = mf.PerformMove("Right");
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow)) && Time.timeScale != 0)
        {
            move = mf.PerformMove("Up");
        }
        else if ((Input.GetKeyDown(KeyCode.X)) && Time.timeScale != 0)
        {
            move = mf.PerformMove("PosX");
        }
        else if ((Input.GetKeyDown(KeyCode.S)) && Time.timeScale != 0)
        {
            Spawner.instance.FallDown = true;
        }




        if (SceneManager.GetActiveScene().buildIndex == 2 && Game2DManager.instance.Selector.activeSelf)
        {
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.UpArrow) && cntr < 20)
            {
                cntr++;
                Game2DManager.instance.Selector.transform.position += new Vector3(0, 30, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && cntr > 0)
            {
                cntr--;
                Game2DManager.instance.Selector.transform.position -= new Vector3(0, 30, 0);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerPrefs.SetInt("CleanRow", cntr);
                move = mf.PerformMove("CleanRow");
                Game2DManager.instance.Selector.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    void Awake()
    {
        this.mf = new MoveFactory();
    }
    public void MoveBlock(string direction)
    {
        move = mf.PerformMove(direction);
    }

    public void UseBomb()
    {
        Player.item[0]--;
        Debug.LogError(Player.item[0]);
        move = mf.PerformMove("Bomb");
    }
    public void UseCleanRow()
    {
        Game2DManager.instance.Selector.SetActive(true);
        Player.item[1]--;
    }
    private void Update()
    {
        CheckInput();
    }

}