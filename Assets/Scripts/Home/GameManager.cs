using System;
using System.Collections;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MuteButton, PlayButton, VibrateButton, SilentButton, InfoButton, BackToHomeButton, BackButton2, LeaderButton, InfoPanel, ModesPage, ModesPanel, SinglePanel, MultiPanel, ConfirmPanel;
    public GameObject SettingsButton, ShopButton, ShopPanel, PlayScreen, TextMeshHome, SettingsPanel, leaderboardWindow, AccountBtnIcon, Camera, FbIcn, AnonyIcn;
    public AudioMixer audioMixer;
    public DatabaseSave ds;
    public Slider volumeslider;
    public float premute, speed = 50f;
    public bool mute = false, Vibrate = false;
    public GameObject AvatarIcn, BtnAvatar, avatarpanel;
    public RectTransform scrollavatar, scrollshop;
    public AudioSource Audio;
    public static GameManager instance;
    private bool beingHandled = false;
    public GameObject AvatarPrefab;
    public void Awake()
    {
        ds.WriteDataFirebaseAsync();
        /* PlayerData Pdata = SaveSystem.LoadPdata ();
        if (Pdata != null) {
            Player.item = Pdata.items;
            //Player.avatar = Pdata.avatars;
            Player.coin = Pdata.coins;
            Debug.LogError (Player.item.Count + "" + Player.avatar.Count + "" + Player.coin);
        } */
    }
    private void Start()
    {
        for (int i = 0; i < 100; i++)
            Player.item.Add(0);
            
        if (PlayerPrefs.GetInt("MusicActive", 1) == 0)
        {
            Audio.mute = true;
            MuteAndPlayMusic(true);
        }
        else
        {
            Audio.mute = false;
            MuteAndPlayMusic(false);
        }
        volumeslider.value = PlayerPrefs.GetFloat("volume", 1);

        PlayerData data = SaveSystem.LoadPdata();
        if (data != null)
        {
            for (int i = 0; i < data.items.Count; i++)
                Player.item[i] = data.items[i];
            Player.coin += data.coins;
        }

        /* if (data != null) {
            volumeslider.value = data.soundlevel;
        } */

    }

    public void MuteAndPlayMusic(bool mute)
    {
        if (mute)
        {
            MuteButton.SetActive(true);
            PlayButton.SetActive(false);
            premute = volumeslider.value;
            PlayerPrefs.SetFloat("premute", premute);
            volumeslider.value = -80;
            PlayerPrefs.SetInt("MusicActive", 0);

        }
        else
        {
            MuteButton.SetActive(false);
            PlayButton.SetActive(true);
            volumeslider.value = PlayerPrefs.GetFloat("premute", 0);
            PlayerPrefs.SetInt("MusicActive", 1);
        }
    }

    public void VibrateSwitch(bool y)
    {
        if (y)
        {
            VibrateButton.SetActive(true);
            SilentButton.SetActive(false);
            /* Handheld.Vibrate();
            Vibrate = true; */
        }
        else
        {
            VibrateButton.SetActive(false);
            SilentButton.SetActive(true);
            Vibrate = false;
        }
    }

    public void InfoPage(bool y)
    {
        if (y)
        {
            PlayScreen.SetActive(false);
            InfoButton.SetActive(false);
            ShopButton.SetActive(false);
            BtnAvatar.SetActive(false);
            BackToHomeButton.SetActive(true);
            InfoPanel.SetActive(true);
            TextMeshHome.SetActive(false);
        }
        else
        {
            PlayScreen.SetActive(true);
            InfoButton.SetActive(true);
            ShopButton.SetActive(true);
            BtnAvatar.SetActive(true);
            BackToHomeButton.SetActive(false);
            InfoPanel.SetActive(false);
            TextMeshHome.SetActive(true);
        }
    }

    public void ShowModesPage(bool y)
    {
        if (y)
        {
            PlayScreen.SetActive(false);
            InfoButton.SetActive(false);
            TextMeshHome.SetActive(false);
            LeaderButton.SetActive(false);
            SettingsButton.SetActive(false);
            AccountBtnIcon.SetActive(false);
            ShopButton.SetActive(false);
            BtnAvatar.SetActive(false);
            BackToHomeButton.SetActive(true);
            ModesPage.SetActive(true);
        }
        else
        {
            BackToHomeButton.SetActive(false);
            ModesPage.SetActive(false);
            PlayScreen.SetActive(true);
            InfoButton.SetActive(true);
            TextMeshHome.SetActive(true);
            LeaderButton.SetActive(true);
            SettingsButton.SetActive(true);
            AccountBtnIcon.SetActive(true);
            ShopButton.SetActive(true);
            BtnAvatar.SetActive(true);
        }
    }

    public void ShowSettingsPanel(bool t)
    {
        if (t)
        {
            //InfoButton.SetActive (false);
            //SettingsButton.SetActive (false);
            //PlayScreen.SetActive (false);
            //AccountBtnIcon.SetActive (false);
            SettingsPanel.SetActive(true);
            LeaderButton.GetComponent<Button>().interactable = false;
            Camera.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            SettingsPanel.SetActive(false);
            //InfoButton.SetActive (true);
            //SettingsButton.SetActive (true);
            //PlayScreen.SetActive (true);
            //AccountBtnIcon.SetActive (true);
            LeaderButton.GetComponent<Button>().interactable = true;
            Camera.GetComponent<CameraController>().enabled = true;
        }
    }

    public void ShowLeaderBoardPanel(bool r)
    {
        if (r)
        {
            leaderboardWindow.SetActive(true);
            ShopButton.SetActive(false);
            BtnAvatar.SetActive(false);
            InfoButton.GetComponent<Button>().interactable = false;
            SettingsButton.GetComponent<Button>().interactable = false;
            AccountBtnIcon.GetComponent<Button>().interactable = false;
            LeaderButton.GetComponent<Button>().interactable = false;
            PlayScreen.GetComponentInChildren<Button>().interactable = false;
            Camera.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            leaderboardWindow.SetActive(false);
            ShopButton.SetActive(true);
            BtnAvatar.SetActive(true);
            InfoButton.GetComponent<Button>().interactable = true;
            SettingsButton.GetComponent<Button>().interactable = true;
            PlayScreen.GetComponentInChildren<Button>().interactable = true;
            AccountBtnIcon.GetComponent<Button>().interactable = true;
            LeaderButton.GetComponent<Button>().interactable = true;
            Camera.GetComponent<CameraController>().enabled = true;
        }
    }
    public void ShowSinglePanel(bool y)
    {
        if (y)
        {
            BackToHomeButton.SetActive(false);
            ModesPage.SetActive(false);
            SinglePanel.SetActive(true);
            BackButton2.SetActive(true);
        }
        else
        {
            SinglePanel.SetActive(false);
            BackButton2.SetActive(false);
            BackToHomeButton.SetActive(true);
            ModesPage.SetActive(true);
        }
    }

    public void ShowMultiPanel(bool y)
    {
        if (y)
        {
            BackToHomeButton.SetActive(false);
            ModesPage.SetActive(false);
            MultiPanel.SetActive(true);
            BackButton2.SetActive(true);
        }
        else
        {
            MultiPanel.SetActive(false);
            BackButton2.SetActive(false);
            BackToHomeButton.SetActive(true);
            ModesPage.SetActive(true);
        }
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void DoExit()
    {
        Application.Quit();
    }

    public void ShowConfirm(bool y)
    {
        if (y)
        {
            ConfirmPanel.SetActive(true);
            ShopButton.SetActive(false);
            BtnAvatar.SetActive(false);
            InfoButton.GetComponent<Button>().interactable = false;
            SettingsButton.GetComponent<Button>().interactable = false;
            AccountBtnIcon.GetComponent<Button>().interactable = false;
            LeaderButton.GetComponent<Button>().interactable = false;
            PlayScreen.GetComponentInChildren<Button>().interactable = false;
            Camera.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            ConfirmPanel.SetActive(false);
            ShopButton.SetActive(true);
            BtnAvatar.SetActive(true);
            InfoButton.GetComponent<Button>().interactable = true;
            SettingsButton.GetComponent<Button>().interactable = true;
            PlayScreen.GetComponentInChildren<Button>().interactable = true;
            AccountBtnIcon.GetComponent<Button>().interactable = true;
            LeaderButton.GetComponent<Button>().interactable = true;
            Camera.GetComponent<CameraController>().enabled = true;

        }
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("loadgame", 1);
        LoadScene(2);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        yield return null;
    }

    private void OnDisable()
    {
        //SaveSystem.SaveGame (volumeslider.value);
        PlayerPrefs.SetFloat("volume", volumeslider.value);
        PlayerPrefs.Save();
        SaveSystem.SavePdata();
    }

    private void Update()
    {
        if (volumeslider.value == -80 && !mute)
        {
            PlayButton.SetActive(false);
            MuteButton.SetActive(true);
            Audio.mute = true;
            mute = true;
        }
        if (volumeslider.value > -80 && mute)
        {
            MuteButton.SetActive(false);
            PlayButton.SetActive(true);
            Audio.mute = false;
            mute = false;
        }

        // Check if Back was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (ModesPage.activeSelf || InfoPanel.activeSelf && !beingHandled)
            {
                Camera.GetComponent<CameraController>().SwitchCameraTarget(0);
                InfoPage(false);
                ShowModesPage(false);
                StartCoroutine(HandleIt());
            }
            if (SinglePanel.activeSelf || MultiPanel.activeSelf && !beingHandled)
            {
                Camera.GetComponent<CameraController>().SwitchCameraTarget(1);
                ShowMultiPanel(false);
                ShowSinglePanel(false);
                StartCoroutine(HandleIt());
            }
            if (!beingHandled)
            {
                if (ConfirmPanel.activeSelf)
                {
                    ShowConfirm(false);
                }
                else
                {
                    ShowConfirm(true);
                }
            }

        }

    }

    public void Shopview(bool t)
    {
        if (t)
        {
            InfoButton.SetActive(false);
            SettingsButton.SetActive(false);
            PlayScreen.SetActive(false);
            AccountBtnIcon.SetActive(false);
            ShopButton.SetActive(false);
            BtnAvatar.SetActive(false);
            LeaderButton.SetActive(false);
            ShopPanel.SetActive(true);
            LeaderButton.SetActive(false);
            Camera.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            ShopPanel.SetActive(false);
            InfoButton.SetActive(true);
            SettingsButton.SetActive(true);
            PlayScreen.SetActive(true);
            ShopButton.SetActive(true);
            BtnAvatar.SetActive(true);
            LeaderButton.SetActive(true);
            AccountBtnIcon.SetActive(true);
            Camera.GetComponent<CameraController>().enabled = true;
        }
    }
    public void ShowAvatarList(bool b)
    {
        bool c = false;
        for (int i = 2; i < Player.item.Count; i++)
        {
            if (Player.item[i] == 1)
            {
                c = true;
                break;
            }
        }
        if (b && c)
        {
            avatarpanel.SetActive(true);

            for (int i = 2; i < Player.item.Count; i++)
            {
                if (Player.item[i] != 0)
                {
                    GameObject g = Instantiate(AvatarPrefab, scrollavatar);
                    g.GetComponent<Image>().sprite = scrollshop.GetChild(i).GetChild(0).GetComponent<Image>().sprite;
                    g.GetComponent<Button>().AddEvent(i, loadImage);
                }

            }
            if (AcountManager.instance.isFb)
            {
                GameObject fb = Instantiate(AvatarPrefab, scrollavatar);
                fb.GetComponent<Image>().sprite = AcountManager.instance.FbSprite;
                fb.GetComponent<Button>().AddEvent(0, loadFb);
                BtnAvatar.SetActive(false);

            }

        }

    }
    void loadFb(int klab)
    {
        PlayerPrefs.SetInt("avatar", 0);
        AvatarIcn.GetComponent<Image>().sprite = AcountManager.instance.FbSprite;
    }
    void loadImage(int index)
    {
        PlayerPrefs.SetInt("avatar", 1);
        PlayerPrefs.SetInt("avatarindx", index);
        avatarpanel.SetActive(false);
        AvatarIcn.GetComponent<Image>().sprite = scrollshop.GetChild(index).GetChild(0).GetComponent<Image>().sprite;
        FbIcn.SetActive(false);
        AnonyIcn.SetActive(false);
        AvatarIcn.SetActive(true);
        foreach (Transform child in scrollavatar.transform)
        {
            Destroy(child.gameObject);
        }
        //Debug.Log("sadasdadsa");
        BtnAvatar.SetActive(true);
    }
    private IEnumerator HandleIt()
    {
        beingHandled = true;
        // process pre-yield
        yield return new WaitForSeconds(1.0f);
        // process post-yield
        beingHandled = false;
    }
}