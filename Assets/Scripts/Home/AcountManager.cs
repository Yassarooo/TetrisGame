using System.Collections;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcountManager : MonoBehaviour
{
    public GameObject AcountPanel, AccountButton, LoginPanel, LoadingCircle, LoadingCircle2, Faildtxt;
    public GameObject FBLogoutButton, AnonyLogoutButton, InfoButton, SettingsButton, LeaderButton, PlayScreen, ShowPP, BtnAvatar, ShopButton, Camera;
    public Text UserName;
    public RawImage PicPanel, AccountBtnIcon, AnonyIcon;
    public Sprite FbSprite;
    public UserData data;
    public LoginManager loginManager;
    public bool isFb;

    #region Singleton
    public static AcountManager instance;
    public AcountManager()
    {
        instance = this;
    }
    #endregion

    private void Awake()
    {
        data = SaveSystem.LoadUser();
        if (data != null)
        {
            if (data.isFB)
            {
                this.isFb = true;
                LoadFbUser();
            }
            if (data.isAnony)
            {
                LoadAnonymousUser();
            }
        }
        else
        {
            LoadNoUser();
        }

        if (PlayerPrefs.GetInt("avatar", 0) == 1)
        {
            LoadAvatar();
        }

        loginManager.LoadingCircle = this.LoadingCircle;
        loginManager.Faildtxt = this.Faildtxt;
    }
    public void ShowAcountPanel(bool e)
    {
        if (e)
        {
            AcountPanel.SetActive(true);
            AccountButton.SetActive(false);
            InfoButton.SetActive(false);
            SettingsButton.SetActive(false);
            LeaderButton.SetActive(false);
            PlayScreen.SetActive(false);
            ShopButton.SetActive(false);
            BtnAvatar.SetActive(false);
            Camera.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            AcountPanel.SetActive(false);
            AccountButton.SetActive(true);
            InfoButton.SetActive(true);
            SettingsButton.SetActive(true);
            LeaderButton.SetActive(true);
            ShopButton.SetActive(true);
            BtnAvatar.SetActive(true);
            PlayScreen.SetActive(true);
            Camera.GetComponent<CameraController>().enabled = true;
        }
    }

    public void LoadAnonymousUser()
    {
        UserName.text = "Guest" + data.ID.Substring(1, 6);
        AccountBtnIcon.texture = AnonyIcon.texture;
        PicPanel.texture = AnonyIcon.texture;
        ShowPP.SetActive(true);
        LoginPanel.SetActive(false);
        AnonyLogoutButton.SetActive(true);
    }

    public void LoadFbUser()
    {
        UserName.text = data.Name;
        StartCoroutine(DownloadImage(data.Photo_url, PicPanel));
        StartCoroutine(DownloadImage(data.Photo_url, AccountBtnIcon));
        LoginPanel.SetActive(false);
        ShowPP.SetActive(true);
        FBLogoutButton.SetActive(true);
    }

    public void LoadAvatar()
    {
        AccountBtnIcon.texture = Shop.instance.shopitemlist[PlayerPrefs.GetInt("avatarindx", -1)].image.texture;
    }

    public void LoadNoUser()
    {
        LoginPanel.SetActive(true);
    }

    public void FacebookLogin()
    {
        loginManager.FacebookLogin();
    }

    public void GuestLogin()
    {
        loginManager.GuestLogin();
    }

    public void DeleteDataAndSignOut()
    {
        LoadingCircle.SetActive(true);
        SaveSystem.DeleteUsrData();
        if (data.isFB)
        {
            loginManager.FacebookLogout();
        }
        LoadScene(0);
    }
    public void LoadScene(int sceneIndex)
    {
        LoadingCircle2.SetActive(true);
        LoadingCircle.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        yield return null;
    }
    IEnumerator DownloadImage(string MediaUrl, RawImage myrawimg)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            myrawimg.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            FbSprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(0.0f, 0.0f, ((DownloadHandlerTexture)request.downloadHandler).texture.width, ((DownloadHandlerTexture)request.downloadHandler).texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
    private void Update()
    {
        if (loginManager.finished)
        {
            LoadScene(1);
            loginManager.finished = false;
        }
    }
}