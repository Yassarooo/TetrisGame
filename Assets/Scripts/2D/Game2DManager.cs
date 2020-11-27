using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game2DManager : MonoBehaviour
{
    public TextMeshProUGUI point;
    public TextMeshProUGUI level;
    public TextMeshProUGUI lines;
    public int _lines = 0;
    public GameObject stopButton;
    public GameObject playButton;
    public GameObject stopMusicButton;
    public GameObject playMusicButton;

    public GameObject RefreshDialog;
    private bool beingHandled = false;

    public AudioSource FullLine, Move, main, Hit, Pause, Resume, Invalid;

    private Block _activeBlock;
    public GameObject gameOverPanel, PausePanel, ThemePanel, upgrid, downcube, Selector;
    public Text FinalScore, coins;

    //public bool EnableWood, DisableWood;
    public Material[] WallMats;

    public GameObject[] Woodblocks, ClassicBlocks;
    System.Random random = new System.Random();
    public Text TxtBomb, TxtCleanRow;
    public Button Bomb, CleanRow;

    public MoveFactory mf;
    Move move;

    #region Singleton
    public static Game2DManager instance;
    public Game2DManager()
    {
        instance = this;
    }
    #endregion
    public void Awake()
    {
        Grid.width = 10;
        

        if (PlayerPrefs.GetInt("loadgame", 0) == 1)
        {
            GameData data = SaveSystem.LoadGame();
            Vector3 SpawnPoint;
            Grid.grid = new Transform[Spawner.instance.gridSizeX, Spawner.instance.gridSizeY, Spawner.instance.gridSizeZ];
            if (data != null)
            {
                Debug.Log("data != null");
                for (int y = 0; y < Spawner.instance.gridSizeY; y++)
                {
                    for (int j = 0; j < Spawner.instance.gridSizeX; j++)
                    {
                        for (int z = 0; z < Spawner.instance.gridSizeZ; z++)
                        {
                            if (data.idxs[j, y, z].x != -1 && data.idxs[j, y, z].y != -1 && data.idxs[j, y, z].z != -1)
                            {
                                if (data.names[j, y, z] != null)
                                {
                                    SpawnPoint.x = data.posis[j, y, z].x;
                                    SpawnPoint.y = data.posis[j, y, z].y;
                                    SpawnPoint.z = data.posis[j, y, z].z;
                                    if (data.names[j, y, z] == "i")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[0].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "i";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                    if (data.names[j, y, z] == "j")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[1].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "j";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                    if (data.names[j, y, z] == "l")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[2].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "l";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                    if (data.names[j, y, z] == "o")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[3].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "o";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                    if (data.names[j, y, z] == "rz")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[4].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "rz";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                    if (data.names[j, y, z] == "t")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[5].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "t";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                    if (data.names[j, y, z] == "z")
                                    {
                                        Transform jazara = Instantiate(Spawner.instance.blocks[6].transform.GetChild(0), SpawnPoint, Quaternion.identity);
                                        jazara.name = "z";
                                        Grid.grid[j, y, z] = jazara;
                                    }
                                }
                            }
                        }
                    }
                }
                //Block.instance.transform.position = data.activeBlock.transform.position;
                StartCoroutine(HandleIt());
            }
            if (data == null)
            {
                Debug.Log("Data is Null !!");
            }
        }

        PlayerPrefs.SetInt("point", 0);
        PlayerPrefs.SetInt("lines", 0);
        PlayerPrefs.SetInt("level", 1);
        ApplyTheme(PlayerPrefs.GetInt("theme", 0));
        if (PlayerPrefs.GetInt("theme", 0) == 10)
        {
            Spawner.instance.blocks = this.Woodblocks;
        }
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("playMusicActive", 1) == 0)
            MuteAndPlayMusic(false);
        else
            MuteAndPlayMusic(true);

        SetScore(0);

        // if ((Screen.height == 2960 && Screen.width == 1440) || (Screen.height == 2340 && Screen.width == 1080)) {
        //     Camera.main.fieldOfView = 70;
        // }
    }

    private void Start()
    {
        
        Player.item.Add(3);
        Player.item.Add(3);
        /* PlayerData Pdata = SaveSystem.LoadPdata ();
        if (Pdata != null) {
            Player.item = Pdata.items;
            //Player.avatar = Pdata.avatars;
            Player.coin = Pdata.coins;
            Debug.LogError (Player.item.Count + "" + Player.coin);
        } */
    }

    public void Update()
    {

        /* if (EnableWood) {
            Spawner.instance.blocks = this.Woodblocks;
        }
         if(DisableWood){
             Spawner.instance.blocks = this.ClassicBlocks;
             DisableWood = false;
         } */
        TxtBomb.text = Player.item[0].ToString();
        TxtCleanRow.text = Player.item[1].ToString();
        coins.text = Player.coin.ToString();

        if (Player.item[0] == 0)
        {
            Bomb.interactable = false;
        }
        if (Player.item[1] == 0)
        {
            CleanRow.interactable = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel.activeSelf || ThemePanel.activeSelf)
            {
                ShowPausePanel(false);
                ShowThemePanel(false);
            }
            else
            {
                ShowPausePanel(true);
            }
        }
        SetFallSpeed();

    }

    public void StopAndPlay()
    {
        if (Time.timeScale == 0)
        {
            if (stopButton != null)
            {
                stopButton.SetActive(true);
            }
            if (playButton != null)
            {
                playButton.SetActive(false);
            }

            Time.timeScale = 1;
        }
        else
        {
            if (stopButton != null)
            {
                stopButton.SetActive(false);
            }
            if (playButton != null)
            {
                playButton.SetActive(true);
            }
            Time.timeScale = 0;
        }
    }
    public void LoadScene(int idx)
    {
        PlayerPrefs.SetInt("point", 0);
        PlayerPrefs.SetInt("lines", 0);
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(idx);
        StartCoroutine(HandleIt());
    }
    public void ShowRefreshDialog(bool y)
    {
        if (y)
        {
            RefreshDialog.SetActive(true);
            StopAndPlay();
        }
        else
        {
            RefreshDialog.SetActive(false);
            StopAndPlay();
        }
    }

    public void ShowPausePanel(bool y)
    {
        if (y)
        {
            PausePanel.SetActive(true);
            StopAndPlay();
        }
        else
        {
            PausePanel.SetActive(false);
            StopAndPlay();
        }
    }

    public void MuteAndPlayMusic(bool mute)
    {
        if (mute)
        {
            stopMusicButton.SetActive(true);
            playMusicButton.SetActive(false);
            PlayerPrefs.SetInt("playMusicActive", 1);
        }
        else
        {
            stopMusicButton.SetActive(false);
            playMusicButton.SetActive(true);
            PlayerPrefs.SetInt("playMusicActive", 0);
        }
    }
    public void ShowThemePanel(bool y)
    {
        if (y)
        {
            ThemePanel.SetActive(true);
        }
        else
        {
            ThemePanel.SetActive(false);
        }
    }
    public void ApplyTheme(int idx)
    {
        PlayerPrefs.SetInt("theme", idx);
        upgrid.GetComponent<MeshRenderer>().material = WallMats[idx];
        downcube.GetComponent<MeshRenderer>().material = WallMats[idx + 1];
        switch (idx)
        {
            case 0:
                Camera.main.backgroundColor = new Color(0, 0.2f, 0.282353f, 1);
                break;
            case 2:
                Camera.main.backgroundColor = new Color(0.1960784f, 0, 0.1137255f, 1);
                break;
            case 4:
                Camera.main.backgroundColor = new Color(0.1686275f, 0, 0.03137255f, 1);
                break;
            case 6:
                Camera.main.backgroundColor = new Color(0, 0.1960784f, 0.1568628f, 1);
                break;
            case 8:
                Camera.main.backgroundColor = new Color(0.02f, 0.02f, 0.02f, 1);
                break;
            case 10:
                Camera.main.backgroundColor = new Color(0.09803922f, 0.09803922f, 0.09803922f, 1);
                Spawner.instance.blocks = this.Woodblocks;
                break;
            case 12:
                Camera.main.backgroundColor = new Color(0.2196078f, 0, 0.1176471f, 1);
                break;
        }

        /* foreach (Transform children in EmptyShader.transform) {
            foreach (Transform child2 in children.transform) {
                child2.GetComponent<MeshRenderer> ().material = WallMats[idx];
            }
        } */
    }
    public void PlaySound(int x)
    {
        switch (x)
        {
            case 1:
                Pause.Play();
                break;
            case 2:
                Resume.Play();
                break;
            case 3:
                Hit.Play();
                break;
            case 4:
                Move.Play();
                break;
            case 5:
                Invalid.Play();
                break;

        }
    }
    public void SetScore(int Multiple)
    {
        if (point != null)
            point.text = "SCORE \n" + PlayerPrefs.GetInt("point", 0);
        if (level != null)
            level.text = "LEVEL \n" + PlayerPrefs.GetInt("level", 1);
        if (lines != null)
        {
            lines.text = "LINES \n" + PlayerPrefs.GetInt("lines", 0);
            if (PlayerPrefs.GetInt("lines", 0) != _lines)
            {
                //FullLine.Play ();
                /* if (GameManager.instance.Vibrate) {
                    Handheld.Vibrate ();
                } */
            }
        }

        if (Multiple == 4)
        {
            PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + (1200 * PlayerPrefs.GetInt("level", 1)));
            Player.coin = Player.coin + 12;
        }
        else if (Multiple == 3)
        {
            PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + (300 * PlayerPrefs.GetInt("level", 1)));
            Player.coin = Player.coin + 3;
        }
        else if (Multiple == 2)
        {
            PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + (100 * PlayerPrefs.GetInt("level", 1)));
            Player.coin = Player.coin + 1;
        }
        else if (Multiple == 1)
        {
            PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + (40 * PlayerPrefs.GetInt("level", 1)));
        }

    }
    void SetFallSpeed()
    {
        if (PlayerPrefs.GetInt("level", 1) == 1)
            Block.instance.fallTime = 1f;
        else if (PlayerPrefs.GetInt("level", 1) == 2)
            Block.instance.fallTime = .8f;
        else if (PlayerPrefs.GetInt("level", 1) == 3)
            Block.instance.fallTime = .6f;
        else if (PlayerPrefs.GetInt("level", 1) == 4)
            Block.instance.fallTime = .5f;
        else if (PlayerPrefs.GetInt("level", 1) == 5)
            Block.instance.fallTime = .4f;
        else if (PlayerPrefs.GetInt("level", 1) == 6)
            Block.instance.fallTime = .3f;
        else if (PlayerPrefs.GetInt("level", 1) == 7)
            Block.instance.fallTime = .2f;
        else
            Block.instance.fallTime = .1f;

        if (Time.timeScale == 0)
            return;
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
        Debug.Log("Saved Game");
    }

    private IEnumerator HandleIt()
    {
        beingHandled = true;
        // process pre-yield
        yield return new WaitForSeconds(1.0f);
        // process post-yield
        beingHandled = false;
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("loadgame", 0);
        SaveSystem.SavePdata();
    }
}