using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    public TextMeshProUGUI level;
    public TextMeshProUGUI lines;
    public int _level = 1, _lines = 0;
    public GameObject stopButton;
    public GameObject playButton;
    public GameObject stopMusicButton;
    public GameObject playMusicButton;
    public Spawner sp;
    public GameObject RefreshDialog;
    public GameObject gameEndpanel;
    public Player2 p1, p2;

    public AudioSource FullLine, Move, main, Hit, Pause, Resume, Invalid;

    //private Block _activeBlock;
    public GameObject gameOverPanel, PausePanel, ThemePanel, upgrid, downcube, _activeObject, ThemeButton;
    public Text FinalScore;

    //public bool EnableWood, DisableWood;
    public Material[] WallMats;

    public GameObject[] Woodblocks, ClassicBlocks;
    System.Random random = new System.Random ();

    #region Singleton
    public static GamePlayManager instance;
    public GamePlayManager () {
        instance = this;
    }
    #endregion
    public void Awake () {
        
        PlayerPrefs.SetInt ("point", 0);
        PlayerPrefs.SetInt ("lines", 0);
        PlayerPrefs.SetInt ("level", 1);
       // ApplyTheme (PlayerPrefs.GetInt ("theme", 0));
        if(PlayerPrefs.GetInt ("theme", 0)==10){
            sp.blocks = this.Woodblocks;
        }
        PlayerPrefs.Save ();
        if (PlayerPrefs.GetInt ("playMusicActive", 1) == 0)
            MuteAndPlayMusic (false);
        else
            MuteAndPlayMusic (true);
        if (gameEndpanel != null)
            gameEndpanel.SetActive(false);

    }
    public void Update () {

        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (PausePanel.activeSelf || ThemePanel.activeSelf) {
                ShowPausePanel (false);
               // ShowThemePanel (false);
            } else {
                ShowPausePanel (true);
            }
        }

        if (p1.enabled == false && p2.enabled == false)
            gameEnd();
    }

    public void gameEnd()
    {
        if (p1.score > p2.score)
        {
            p1.win.SetActive(true);
            p2.lose.SetActive(true);
        }
        else if (p1.score < p2.score)
        {
            p2.win.SetActive(true);
            p1.lose.SetActive(true);
        }
        else
        {
            p1.draw.SetActive(true);
        }
        gameEndpanel.SetActive(true);
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
            RefreshDialog.SetActive (true);
            StopAndPlay ();
        } else {
            RefreshDialog.SetActive (false);
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