using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveUser(LoginManager fb, int Score)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/UserData.corona";
        FileStream stream = new FileStream(path, FileMode.Create);

        UserData data = new UserData(fb, Score);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static UserData LoadUser()
    {
        string path = Application.persistentDataPath + "/UserData.corona";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserData data = formatter.Deserialize(stream) as UserData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("UserData File Not Found ! ");
            return null;
        }
    }

    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/GameData.corona";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/GameData.corona";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("GameData File Not Found ! ");
            return null;
        }
    }
    public static void SavePdata()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerData.corona";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPdata()
    {
        string path = Application.persistentDataPath + "/PlayerData.corona";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("PlayerData File Not Found ! ");
            return null;
        }
    }

    public static void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/GameData.corona";
        File.Delete(path);
        // DirectoryInfo directory = new DirectoryInfo (path);
        // directory.Delete (true);
        // Directory.CreateDirectory (path);
    }

    public static void DeleteUsrData()
    {
        string path = Application.persistentDataPath + "/UserData.corona";
        File.Delete(path);
        // DirectoryInfo directory = new DirectoryInfo (path);
        // directory.Delete (true);
        // Directory.CreateDirectory (path);
    }
}