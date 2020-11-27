using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData {
    public string Photo_url, Name, Email, ID, FbToken;
    public bool isFB, isAnony;
    public int Score;
    public UserData (LoginManager usr, int Score) {
        this.isFB = usr.isFB;
        this.isAnony = usr.isAnony;
        this.Photo_url = usr.photo_url;
        this.Name = usr.FireName;
        this.Email = usr.FireEmail;
        this.FbToken = usr.FbToken;
        this.ID = usr.FireId;
        this.Score = Score;
    }
}