using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class GamePlayManager2 : MonoBehaviour {

    public TextMeshProUGUI otherScore;
    public int _level = 1, _lines = 0;
    public GameObject stopButton;
    public GameObject playButton;
    public GameObject stopMusicButton;
    public GameObject gameEndpanel;
    public GameObject playMusicButton;
    public GameObject win,lose,draw;
    public Player2 p1;
    private bool IsGameReady = false;
    public List<PlayerCreate> players = new List<PlayerCreate>();
    public PlayerCreate LocalPlayer;

    public AudioSource FullLine, Move, main, Hit, Pause, Resume, Invalid;

    //private Block _activeBlock;
    public GameObject gameOverPanel, PausePanel, ThemePanel, upgrid, downcube, _activeObject, ThemeButton;
    public Text FinalScore;

    //public bool EnableWood, DisableWood;
    public Material[] WallMats;

    public GameObject[] Woodblocks, ClassicBlocks;
    System.Random random = new System.Random ();

    #region Singleton
    public static GamePlayManager2 instance;
    public GamePlayManager2 () {
        instance = this;
    }
    #endregion
    public void Awake () {
        if (PlayerPrefs.GetInt ("playMusicActive", 1) == 0)
            MuteAndPlayMusic (false);
        else
            MuteAndPlayMusic (true);
        if (gameEndpanel != null)
            gameEndpanel.SetActive(false);
        p1.enabled = false;

    }
    public void Update () {

        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (PausePanel.activeSelf || ThemePanel.activeSelf) {
                ShowPausePanel (false);
            } else {
                ShowPausePanel (true);
            }
        }

        if (NetworkManager.singleton.isNetworkActive)
        {
            GameReadyCheck();
            GameOverCheck();
            FindLocalPlayer();
            if(LocalPlayer!= null)
            {
                UpdateStats();
                updatescore();
            }
        }
        else
        {
            //Cleanup state once network goes offline
            IsGameReady = false;
            LocalPlayer = null;
            players.Clear();
        }

    }

    void updatescore()
    {
        UnityEngine.Debug.Log("players" + players.Count);
        foreach (PlayerCreate play in players)
        {
            if (play != LocalPlayer)
            {
                otherScore.text ="Enemy Score\n" + play.score;
            }
        }
    }

    void GameReadyCheck()
    {
        if (!IsGameReady)
        {
            //Look for connections that are not in the player list
            foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned)
            {
                PlayerCreate comp = kvp.Value.GetComponent<PlayerCreate>();

                //Add if new
                if (comp != null && !players.Contains(comp))
                {
                    players.Add(comp);
                }
            }

            

            //If minimum connections has been check if they are all ready
            if (players.Count == 2)
            {
                    IsGameReady = true;
                    p1.enabled = true;
            }
        }
        if (!p1.enabled && IsGameReady)
        {
            LocalPlayer.endgame();
        }
    }

    void GameOverCheck(){
        int cnt = 0;
        PlayerCreate enemy = null;
        foreach (PlayerCreate play in players)
        {
            if (play.end)
                cnt++;
            if (play != LocalPlayer)
                enemy = play;
        }
        if (cnt == 2)
        {
            if (LocalPlayer.score > enemy.score)
            {
                win.SetActive(true);
            }
            if (LocalPlayer.score < enemy.score)
            {
                lose.SetActive(true);
            }
            if (LocalPlayer.score == enemy.score)
            {
                draw.SetActive(true);
            }
            gameEndpanel.SetActive(true);
        }
    }

    void FindLocalPlayer()
    {
        //Check to see if the player is loaded in yet
        if (ClientScene.localPlayer == null)
            return;

        LocalPlayer = ClientScene.localPlayer.GetComponent<PlayerCreate>();
    }

    public void gameEnd()
    {

    }

    void UpdateStats()
    {
        LocalPlayer.set(p1.score);
    }

    public void StopAndPlay () {
        if (Time.timeScale == 0) {
            if (stopButton != null) {
                stopButton.SetActive (true);
            }
            if (playButton != null) {
                playButton.SetActive (false);
            }

            Time.timeScale = 1;
        } else {
            if (stopButton != null) {
                stopButton.SetActive (false);
            }
            if (playButton != null) {
                playButton.SetActive (true);
            }
            Time.timeScale = 0;
        }
    }
    public void LoadScene (int idx) {
        PlayerPrefs.SetInt ("point", 0);
        PlayerPrefs.SetInt ("lines", 0);
        PlayerPrefs.SetInt ("level", 1);
        PlayerPrefs.Save ();
        Time.timeScale = 1;
        SceneManager.LoadScene (idx);
    }
    public void ShowRefreshDialog (bool y) {
        if (y) {
            //RefreshDialog.SetActive (true);
            StopAndPlay ();
        } else {
            //RefreshDialog.SetActive (false);
            StopAndPlay ();
        }
    }

    public void ShowPausePanel (bool y) {
        if (y) {
            PausePanel.SetActive (true);
            StopAndPlay ();
        } else {
            PausePanel.SetActive (false);
            StopAndPlay ();
        }
    }

    public void MuteAndPlayMusic (bool mute) {
        if (mute) {
            stopMusicButton.SetActive (true);
            playMusicButton.SetActive (false);
            PlayerPrefs.SetInt ("playMusicActive", 1);
        } else {
            stopMusicButton.SetActive (false);
            playMusicButton.SetActive (true);
            PlayerPrefs.SetInt ("playMusicActive", 0);
        }
    }
   
    public void PlaySound (int x) {
        switch (x) {
            case 1:
                Pause.Play ();
                break;
            case 2:
                Resume.Play ();
                break;
            case 3:
                Hit.Play ();
                break;
            case 4:
                Move.Play ();
                break;
            case 5:
                Invalid.Play ();
                break;

        }
    }
    
}