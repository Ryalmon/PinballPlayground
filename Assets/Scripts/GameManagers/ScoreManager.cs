using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{ 
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
        GameplayParent.Instance.UI.UpdateScoreUI(CurrentScore);
        //_scoreText.text = CurrentScore.ToString();
    }
}
