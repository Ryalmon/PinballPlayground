using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{ 
    [SerializeField] TMP_Text _scoreText;
    internal int CurrentScore;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddToScore(int addedScore)
    {
        CurrentScore += addedScore;
        UpdateText();
    }

    private void UpdateText()
    {
        //Displays the score in the game world
        _scoreText.text = CurrentScore.ToString();
    }
}
