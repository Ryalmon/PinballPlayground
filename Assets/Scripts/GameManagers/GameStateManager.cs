using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab;

    internal static GamePlayState GPS = GamePlayState.Intro;

    public enum GamePlayState { 
        Intro,
        Play,
        End
    };


    private void Awake()
    {

        

    }

    public void StartGame()
    {
        //Set currentgameplay state to play
        GPS = GamePlayState.Play;
        //Starts the timer
        GameplayParent.Instance.Timer.StartCountdown();
    }

    public void EndGameState()
    {
        GPS = GamePlayState.End;
        GameplayParent.Instance.UI.GameEndUI();
    }

    public void SpawnBall()
    {
        //Places a ball
        Instantiate(BallPrefab, Vector2.zero, Quaternion.identity);

        //---Currently the ball is placed at (0,0) this will change later!---
    }
}
