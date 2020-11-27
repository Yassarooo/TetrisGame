using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    DatabaseReference reference;
    public string FireName, FireEmail, FireId, photo_url;
    public GameObject btnLogin, btnGuest, LoadingCircle, Faildtxt, txtLogin;
    public bool finished = false, isFB = false, isGuest = false, isAnony;

    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.Credential credential;
    public Firebase.Auth.FirebaseUser newUser, user;
    public string FbToken;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }

        if (FB.IsLoggedIn)
        {
            //GetName ();
            //FB.API ("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
        }
        else
        {
            //txtStatus.text = "Please login to continue.";
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void FacebookLogin()
    {
        LoadingCircle.SetActive(true);
        isFB = true;
        var permissions = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            FbToken = aToken.TokenString;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
                Debug.Log(perm);
        }
        else
        {
            LoadingCircle.SetActive(false);
            Faildtxt.SetActive(true);
            Debug.Log("User Cancelled Login");
        }

        FirebaseLogin();
    }

    public async void FirebaseLogin()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        credential = Firebase.Auth.FacebookAuthProvider.GetCredential(FbToken);
        bool isError = false;
        await auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                isError = true;
                LoadingCircle.SetActive(false);
                Faildtxt.SetActive(true);
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                isError = true;
                //LoadingCircle.SetActive (false);
                //Faildtxt.SetActive (true);
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }
            newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });

        if (!isError)
        {

            user = auth.CurrentUser;
            if (user != null)
            {
                Debug.Log("Entered (user != null)");
                FireName = user.DisplayName;
                FireEmail = user.Email;
                FireId = user.UserId;
                photo_url = user.PhotoUrl.OriginalString;
                isFB = true;
                if (PlayerPrefs.GetInt("firsttime", -1) == 0)
                {
                    PlayerPrefs.SetInt("firsttime", 1);
                    Player.coin += 50;
                }
                SaveSession();
                finished = true;
            }
        }
    }

    public async void GuestLogin()
    {
        LoadingCircle.SetActive(true);
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        bool isError = false;

        await auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                isError = true;
                LoadingCircle.SetActive(false);
                Faildtxt.SetActive(true);
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                isError = true;
                LoadingCircle.SetActive(false);
                Faildtxt.SetActive(true);
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Anonymous signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

        if (!isError)
        {
            user = auth.CurrentUser;
            if (user != null)
            {
                Debug.Log("Entered (user != null)");
                FireName = user.DisplayName;
                FireId = user.UserId;
                isFB = false;
                isAnony = true;
                SaveSession();
                finished = true;
            }
        }

    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }

    void Start()
    {
        //Load Previous Session
        LoadSession();
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tetris-co.firebaseio.com/");
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveSession()
    {
        SaveSystem.SaveUser(this, PlayerPrefs.GetInt("HiScore", 0));
    }

    public void LoadSession()
    {
        UserData data = SaveSystem.LoadUser();

        if (data != null)
        {
            btnLogin.GetComponent<Button>().interactable = false;
            btnGuest.GetComponent<Button>().interactable = false;
            LoadingCircle.SetActive(true);
            finished = true;
        }
        else
        {
            txtLogin.SetActive(true);
            return;
        }
    }

    public void LoadScene(int sceneIndex)
    {
        LoadingCircle.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        yield return null;
    }

    public void Update()
    {
        if (finished)
        {
            LoadScene(1);
            finished = false;
        }
    }

}