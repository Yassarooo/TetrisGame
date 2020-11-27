using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game3DManager : MonoBehaviour {
    public static Game3DManager instance;
    public GameObject gameOverPanel;

    int layersCleared;
    bool gameIsOver = false;
    public bool highspeed = false;
    private bool beingHandled = false;
    public Text FinalScore;
    public Text scoreText;
    public Text levelText;
    public Text layerText;
    public AudioSource Move, Hit;
    
    public GameObject RefreshDialog;
    void Awake () {
        instance = this;
        Grid.width = Spawner.instance.gridSizeX;
    }

    void Start () {
        SetScore (0);
    }

    void Update () {
        SetFallSpeed (highspeed);
    }
    public void SetScore (int Multiple) {

        if (Multiple == 4)
            PlayerPrefs.SetInt ("point", PlayerPrefs.GetInt ("point") + (1200 * PlayerPrefs.GetInt ("level", 1)));
        else if (Multiple == 3)
            PlayerPrefs.SetInt ("point", PlayerPrefs.GetInt ("point") + (300 * PlayerPrefs.GetInt ("level", 1)));
        else if (Multiple == 2)
            PlayerPrefs.SetInt ("point", PlayerPrefs.GetInt ("point") + (100 * PlayerPrefs.GetInt ("level", 1)));
        else if (Multiple == 1)
            PlayerPrefs.SetInt ("point", PlayerPrefs.GetInt ("point") + (40 * PlayerPrefs.GetInt ("level", 1)));

        UpdateUI (PlayerPrefs.GetInt ("point", 0), PlayerPrefs.GetInt ("level", 0), Multiple);
        //update ui
    }

    public void LayersCleared (int amount) {
        if (amount == 1) {
            SetScore (400);
        } else if (amount == 2) {
            SetScore (800);
        } else if (amount == 3) {
            SetScore (1600);
        } else if (amount == 4) {
            SetScore (3200);
        }

        layersCleared += amount;
        // update ui
        UpdateUI (PlayerPrefs.GetInt ("point", 0), PlayerPrefs.GetInt ("level", 0), layersCleared);
    }

    public void SetFallSpeed (bool highspeed) {
        if (highspeed) {
            Block.instance.fallTime = .1f;
            return;
        }
        if (PlayerPrefs.GetInt ("level", 1) == 1)
            Block.instance.fallTime = 3f;
        else if (PlayerPrefs.GetInt ("level", 1) == 2)
            Block.instance.fallTime = 2.75f;
        else if (PlayerPrefs.GetInt ("level", 1) == 3)
            Block.instance.fallTime = 2.5f;
        else if (PlayerPrefs.GetInt ("level", 1) == 4)
            Block.instance.fallTime = 2.25f;
        else if (PlayerPrefs.GetInt ("level", 1) == 5)
            Block.instance.fallTime = 2f;
        else if (PlayerPrefs.GetInt ("level", 1) == 6)
            Block.instance.fallTime = 1.75f;
        else if (PlayerPrefs.GetInt ("level", 1) == 7)
            Block.instance.fallTime = 1.5f;
        else
            Block.instance.fallTime = .1f;

        if (Time.timeScale == 0)
            return;
    }
    public void ShowRefreshDialog (bool y) {
        if (y) {
            RefreshDialog.SetActive (true);
            Time.timeScale = 0;
        } else {
            RefreshDialog.SetActive (false);
            Time.timeScale = 1;
        }
    }
    public void UpdateUI (int score, int level, int layers) {
        scoreText.text = "Score: " + score.ToString ("D9");
        levelText.text = "Level: " + level.ToString ("D2");
        layerText.text = "Layers: " + layers.ToString ("D9");

    }
    public void PlaySound (int x) {
        switch (x) {
            case 3:
                Hit.Play ();
                break;
            case 4:
                Move.Play ();
                break;

        }
    }

    public void LoadScene (int idx) {
        Time.timeScale = 1;
        SceneManager.LoadScene (idx);
        StartCoroutine (HandleIt ());
    }

    private IEnumerator HandleIt () {
        beingHandled = true;
        // process pre-yield
        yield return new WaitForSeconds (1.0f);
        // process post-yield
        beingHandled = false;
    }

}