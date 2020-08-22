using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// This is a static class, so everything in it must be static
static public class SaveGameManager
{
    // ———————————————— Statics ———————————————— //
    // I've chosen not to use SNAKE_CASE for statics in this class because everything is static.
    static private SaveFile saveFile;
    static private string filePath;
    // LOCK, if true, prevents the game from saving. This avoids issues that can
    //  happen while loading files.
    static public bool LOCK
    {
        get;
        private set;
    }


    static SaveGameManager()
    {
        LOCK = false;
        filePath = Application.persistentDataPath + "/EndlessFaller.save";

        saveFile = new SaveFile();
    }

    static public void Save()
    {
        // If this is LOCKed, don't save
        if (LOCK) return;

        string jsonSaveFile = JsonUtility.ToJson(saveFile, true);

        File.WriteAllText(filePath, jsonSaveFile);
    }

    static public void Load()
    {
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            try
            {
                saveFile = JsonUtility.FromJson<SaveFile>(dataAsJson);
            }
            catch
            {
                Debug.LogWarning("SaveGameManager:Load() – SaveFile was malformed.\n" + dataAsJson);
                return;
            }

            LOCK = false;
        }
        else
        {
            LOCK = false;
        }
    }

    static public void DeleteSave()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            saveFile = new SaveFile();
            Debug.Log("SaveGameManager:DeleteSave() – Successfully deleted save file.");
        }
        else
        {
            Debug.LogWarning("SaveGameManager:DeleteSave() – Unable to find and delete save file!"
                + " This is absolutely fine if you've never saved or have just deleted the file.");
        }

        LOCK = false;
    }


    static internal bool CheckHighScore(int score)
    {
        if (score > saveFile.highScore)
        {
            saveFile.highScore = score;
            return true;
        }
        return false;
    }

    static public int GetHighScore()
    {
        return saveFile.highScore;
    }
}

// A class must be serializable to be converted to and from JSON by JsonUtility.
//[System.Serializable]
public class SaveFile
{
    public int highScore;
}