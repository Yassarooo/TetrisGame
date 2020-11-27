using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Above three namespaces will be created at the creation of .cs file. Add below three namespaces

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderBoardHandler : MonoBehaviour {

    // Firebase Database reference

    public UserData mydata;
    public GameObject leaderboardWindow, ErorrPanel;
    public RectTransform content;
    public GameObject prefab;
    public RawImage myimg;
    public Text myname, myrank, mylevel;

    private void Awake () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tetris-co.firebaseio.com/");

    }

    IEnumerator DownloadImage (string MediaUrl, RawImage myrawimg) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture (MediaUrl);
        yield return request.SendWebRequest ();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log (request.error);
        else
            myrawimg.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
    }

    // Showing the leaderboard
    public void LoadLeaderboard () {
        RectTransform rt = content.GetComponent<RectTransform> ();
        mydata = SaveSystem.LoadUser ();
        if (mydata == null) {
            ErorrPanel.SetActive (true);
            return;
        }
        int tr = 0;
        FirebaseDatabase.DefaultInstance
            .GetReference ("players").OrderByChild ("score")
            .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
                if (e2.DatabaseError != null) {
                    Debug.LogError (e2.DatabaseError.Message);
                    return;
                }
                Debug.Log ("Received values for Leaders.");

                if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
                    // delete previous data
                    foreach (Transform child in content.transform) {
                        Destroy (child.gameObject);
                        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y - 200);
                        tr = 0;
                    }

                    int rank = 0;
                    int pre_score = -1;
                    bool frst = false, scnd = false, thrd = false;

                    foreach (var childSnapshot in e2.Snapshot.Children) {
                        if (childSnapshot.Child ("score") == null ||
                            childSnapshot.Child ("score").Value == null) {
                            Debug.LogError ("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
                            break;
                        } else {
                            Debug.Log ("Leaders entry : " +
                                childSnapshot.Child ("playername").Value.ToString () + " - " +
                                childSnapshot.Child ("score").Value.ToString ());

                            GameObject card = GameObject.Instantiate (prefab.gameObject);

                            if (pre_score != (-int.Parse (childSnapshot.Child ("score").Value.ToString ()))) {
                                rank++;
                            }
                            card.transform.Find ("rank").GetComponent<Text> ().text = rank + "";
                            if (childSnapshot.HasChild ("picurl") && childSnapshot.Child ("picurl").Value.ToString ().Length > 0) {
                                StartCoroutine (DownloadImage (childSnapshot.Child ("picurl").Value.ToString (), card.transform.Find ("profile").GetComponent<RawImage> ()));
                            }
                            if (childSnapshot.HasChild ("isFB") && childSnapshot.Child ("isFB").Value.ToString () == "y") {

                                card.transform.Find ("name").GetComponent<Text> ().text = childSnapshot.Child ("playername").Value.ToString (); //.Split()[0]
                            }
                            if (childSnapshot.HasChild ("isFB") && childSnapshot.Child ("isFB").Value.ToString () == "n") {

                                card.transform.Find ("name").GetComponent<Text> ().text = "GUEST" + childSnapshot.Child ("playername").Value.ToString ().Substring (1, 6); //.Split()[0]
                            }
                            card.transform.Find ("score").GetComponent<Text> ().text = (-int.Parse (childSnapshot.Child ("score").Value.ToString ())).ToString ();

                            if (childSnapshot.Key.Equals (mydata.ID)) {
                                // card.transform.Find ("name").GetComponent<Text> ().color = Color.red;
                                // card.transform.Find ("score").GetComponent<Text> ().color = Color.red;
                                // card.transform.Find ("rank").GetComponent<Text> ().color = Color.red;
                                card.GetComponent<Image> ().color = new Color32 (125, 11, 45, 130);

                                if (mydata.isAnony)
                                    myname.text = "GUEST" + childSnapshot.Child ("playername").Value.ToString ().Substring (1, 6);
                                if (mydata.isFB)
                                    myname.text = childSnapshot.Child ("playername").Value.ToString ();
                                myrank.text = (rank).ToString ();
                                mylevel.text = (-int.Parse (childSnapshot.Child ("score").Value.ToString ())).ToString ();

                                if (childSnapshot.HasChild ("picurl") && childSnapshot.Child ("picurl").Value.ToString ().Length > 0) {
                                    StartCoroutine (DownloadImage (childSnapshot.Child ("picurl").Value.ToString (), myimg));
                                }
                            }

                            pre_score = -int.Parse (childSnapshot.Child ("score").Value.ToString ());

                            card.transform.SetParent (content, false);
                            card.transform.Translate (0, -tr, 0);
                            tr += 300;
                            rt.sizeDelta = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y + 300);

                            if (!frst) {
                                card.GetComponent<Image> ().color = new Color32 (189, 25, 5, 145);
                                card.transform.Find ("1st").gameObject.SetActive (true);
                                card.transform.Find ("crown").gameObject.SetActive (true);
                                frst = true;
                            } else if (frst) {
                                if (!scnd) {
                                    card.GetComponent<Image> ().color = new Color32 (70, 70, 70, 150);
                                    card.transform.Find ("2nd").gameObject.SetActive (true);
                                    scnd = true;
                                } else if (scnd) {
                                    if (!thrd) {
                                        card.GetComponent<Image> ().color = new Color32 (80, 80, 0, 160);
                                        card.transform.Find ("3d").gameObject.SetActive (true);
                                        thrd = true;
                                    }
                                }
                            }

                        }
                    }
                }
            };
        leaderboardWindow.SetActive (true);
    }

}