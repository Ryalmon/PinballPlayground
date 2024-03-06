using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] UnityEvent _gameEndEvent;
    private const int _mainMeunScene = 0;

    internal GamePlayState GPS = GamePlayState.Intro;
    

    public enum GamePlayState { 
        Intro,
        Play,
        End
    };


    public void StartGame()
    {
        if (GPS != GamePlayState.Intro) return;
        //Populates all events
        PopulateEvents();
        //Set currentgameplay state to play
        GPS = GamePlayState.Play;
        //Starts the timer
        GameplayManagers.Instance.Timer.StartCountdown();
        
    }

    private void PopulateEvents()
    {
        PopulateEndEvent();
    }

    private void PopulateEndEvent()
    {
        _gameEndEvent.AddListener(GameplayManagers.Instance.UI.GameEndUI);
        _gameEndEvent.AddListener(GameplayManagers.Instance.Ball.RemoveAllBalls);
    }

    public void EndGameState()
    {
        GPS = GamePlayState.End;
        _gameEndEvent.Invoke();
    }

    public void EndScene()
    {
        UniversalManager.Instance.Scene.LoadScene(_mainMeunScene);
    }
}
