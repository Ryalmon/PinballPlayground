using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager M_Instance;
    public GameSaveData GSD;
    private string path;

    void Awake()
    {
        EstablishSingleton();

        
        Load();

    }

    private void EstablishSingleton()
    {
        if (M_Instance != null && M_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        M_Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void EstablishPath()
    {
        path = Application.isEditor ? Application.dataPath : Application.persistentDataPath; //checks to see if we're in editor, if so use datapath instead of persistent datapath
        path = path + "/SaveData/"; //append /SaveData/ to said path
        if (!Application.isEditor && !Directory.Exists(path)) //check if we're in a build, check if the directory exists, if not
        {
            Directory.CreateDirectory(path); //create it
        }
    }

    public void SavePlayerValues(int position, string name, int score)
    {
        GSD.SaveNames[position - 1] = name;
        GSD.SaveScore[position - 1] = score;
        SaveText();
    }

    public string ReturnPlayerName(int position)
    {
        return GSD.SaveNames[position - 1];
    }

    public int ReturnPlayerScore(int position)
    {
        return GSD.SaveScore[position - 1];
    }

    public void SaveText()
    {
        var convertedJson = JsonConvert.SerializeObject(GSD, Formatting.None, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        File.WriteAllText(path + "Data.json", convertedJson);
    }

    public void Load()
    {
        if (File.Exists(path + "Data.json"))
        {
            var json = File.ReadAllText(path + "Data.json");
            GSD = JsonConvert.DeserializeObject<GameSaveData>(json);
        }
        else
        {
            //Starting Values

            SaveText();
        }
    }
}

[System.Serializable]
public class GameSaveData
{
    public List<string> SaveNames = new List<string>();
    public List<int> SaveScore = new List<int>();
}
