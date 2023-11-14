using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager M_Instance;
    public GameSaveData GSD;
    private string _path;

    void Awake()
    {
        if (EstablishSingleton())
            return;
        EstablishPath();
        Load();

    }

    private bool EstablishSingleton()
    {
        if (M_Instance != null && M_Instance != this)
        {
            Destroy(gameObject);
            return false;
        }

        M_Instance = this;
        DontDestroyOnLoad(gameObject);
        return false;
    }

    private void EstablishPath()
    {
        _path = Application.isEditor ? Application.dataPath : Application.persistentDataPath; //checks to see if we're in editor, if so use datapath instead of persistent datapath
        _path = _path + "/SaveData/"; //append /SaveData/ to said path
        if (!Application.isEditor && !Directory.Exists(_path)) //check if we're in a build, check if the directory exists, if not
        {
            Directory.CreateDirectory(_path); //create it
        }
    }

    public void SavePlayerValues(string name, int score, int pos)
    {
        GSD.SaveNames[pos] = name;
        GSD.SaveScore[pos] = score;
        //SaveText();
    }

    public string ReturnPlayerName(int position)
    {
        return GSD.SaveNames[position - 1];
    }

    public int ReturnPlayerScore(int position)
    {
        return GSD.SaveScore[position - 1];
    }

/*    public string[] ReturnPlayerList()
    {
        return GSD.SaveNames;
    }

    public int[] ReturnPlayerScores()
    {
        return GSD.SaveScore;
    }

    public void FindScorePlacement(string name, int score)
    {
        int prevScore = GSD.SaveScore[9];
        for(int i = 9; i > 0; i++)
        {
            if(score < GSD.SaveScore[i] && score > prevScore)
            {
                
            }

            prevScore = GSD.SaveScore[i];
        }
    }*/

    public void PlaceScoreInArray(string name, int score, int pos)
    {
        if(score > GSD.SaveScore[pos])
        {
            if(pos + 1 < GSD.SaveScore.Length)
            {
                MovePreviousValues(pos);
            }
            SavePlayerValues(name, score, pos);
            PlaceScoreInArray(name, score, --pos);
            return;
        }
        SaveText();
    }

    private void MovePreviousValues(int pos)
    {
        GSD.SaveNames[pos + 1] = GSD.SaveNames[pos];
        GSD.SaveScore[pos + 1] = GSD.SaveScore[pos];
    }

    public void SaveText()
    {
        var convertedJson = JsonUtility.ToJson(GSD);
        File.WriteAllText(_path + "Data.json", convertedJson);
    }

    public void Load()
    {
        if (File.Exists(_path + "Data.json"))
        {
            var json = File.ReadAllText(_path + "Data.json");
            GSD = JsonUtility.FromJson<GameSaveData>(json);
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
    public string[] SaveNames = new string[10];
    public int[] SaveScore = new int[10];
}
