using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class DatabaseSave : MonoBehaviour {
    public UserData mydata;
    public DatabaseReference myDbRef;

    private void Awake () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://tetris-co.firebaseio.com/");
        myDbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void WriteDataFirebaseAsync () {

        mydata = SaveSystem.LoadUser ();
        if (mydata != null) {
            Debug.Log ("Entered writedatafirebase methoooood !!!");
            if (mydata.isFB) {
                StartCoroutine (writePlayer (mydata.ID, mydata.Name, mydata.Email, mydata.Photo_url, "y", mydata.Score));
            }
            if (mydata.isAnony) {
                StartCoroutine (writePlayer (mydata.ID, mydata.ID, mydata.ID, null, "n", mydata.Score));
            }
        } else {
            Debug.Log ("No session saved");
        }
    }

    public class Player {

        public string playername;
        public string email, picurl, isFB;
        public string level = "0";

        public int score = 0;

        public Player (string playername, string email, string picurl, string isFB, int score) {
            this.playername = playername;
            this.email = email;
            this.picurl = picurl;
            this.isFB = isFB;
            this.score = score;
        }
    }

    public IEnumerator writePlayer (string userId, string playername, string email, string picurl, string isFB, int score) {

        Player player = new Player (playername, email, picurl, isFB, score);
        string json = JsonUtility.ToJson (player);

        myDbRef.Child ("players").Child (userId).SetRawJsonValueAsync (json);

        yield return null;
    }
}