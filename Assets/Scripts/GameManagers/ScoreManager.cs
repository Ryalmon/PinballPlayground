using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{ 
    [SerializeField] TMP_Text _scoreText;
    public static ScoreManager M_Instance;
    internal int CurrentScore;
    
    // Start is called before the first frame update
    void Start()
    {
        M_Instance = this;
    }

    public void AddToScore(int addedScore)
    {

    }

    private void UpdateText()
    {
        //Displays the score in the game world
        _scoreText.text = CurrentScore.ToString();
    }
}
