using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveDown : Move
{
    public MoveDown()
    {
        Debug.Log("Entered MooooveeeeDoooooowwwwn");
        float _fallTime = Block.instance.fallTime;
        if ((Input.GetKeyDown(KeyCode.DownArrow)) && Time.timeScale != 0)
        {
            _fallTime = Block.instance.fallTime / 10;
        }

        if (Time.time - Block.previosTime > _fallTime)
        {
        repeat: Block.instance.transform.position += Vector3.down;
            Debug.Log(Block.instance.transform.position);
            if (!ValidMove(Block.instance.transform))
            {
                Spawner.instance.FallDown = false;
                Block.instance.transform.position -= Vector3.down;
                Grid.AddToGrid(Block.instance.transform);
                Grid.CheckForLines();
                Block.instance.enabled = false;
                if (Block.instance.transform.position.y >= Spawner.instance.gridSizeY - 4)
                {
                    Time.timeScale = 0;
                    if (SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        Game2DManager.instance.gameOverPanel.SetActive(true);
                        Game2DManager.instance.FinalScore.text = PlayerPrefs.GetInt("point").ToString();
                    }
                    if (SceneManager.GetActiveScene().buildIndex == 3)
                    {
                        Game3DManager.instance.gameOverPanel.SetActive(true);
                        Game3DManager.instance.FinalScore.text = PlayerPrefs.GetInt("point").ToString();
                    }
                }
                if (!Block.gameRunningStatus && Time.timeScale == 0)
                    return;
                if (SceneManager.GetActiveScene().buildIndex == 3)
                    Game3DManager.instance.highspeed = false;

                Spawner.instance.CreateNewBlock(true);
            }
            if (Spawner.instance.FallDown)
                goto repeat;
            Block.previosTime = Time.time;
        }
    }
}