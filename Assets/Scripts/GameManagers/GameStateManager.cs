using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent _gameStartEvent;
    [SerializeField] UnityEvent _gameEndEvent;

    [SerializeField] UnityEvent _ballActivatedEvent;
    [SerializeField] UnityEvent _ballDeactivatedEvent;
    
    private const int _mainMenuScene = 0;

    internal GamePlayState GPS = GamePlayState.Intro;
    private BallActiveGameplayState BallActiveEnum = BallActiveGameplayState.BallInactive;
    

    public enum GamePlayState { 
        Intro,
        Play,
        End
    };

    public enum BallActiveGameplayState
    {
        BallActive,
        BallInactive
    };

    public void LaunchBallButtonPress()
    {
        if (GPS == GamePlayState.Intro)
            StartGame();

        ActivateBallState();
    }

    public void StartGame()
    {
        //Set currentgameplay state to play
        GPS = GamePlayState.Play;
        //Starts the timer
        _gameStartEvent.Invoke();
    }


    public void EndGameState()
    {
        GPS = GamePlayState.End;
        _gameEndEvent.Invoke();
    }

    public void ActivateBallState()
    {
        BallActiveEnum = BallActiveGameplayState.BallActive;
        _ballActivatedEvent.Invoke();
    }

    public void DeactivateBallState()
    {
        BallActiveEnum = BallActiveGameplayState.BallInactive;
        _ballDeactivatedEvent.Invoke();
    }

    public void EndScene()
    {
        UniversalManager.Instance.Scene.LoadScene(_mainMenuScene);
    }

    public UnityEvent GetGameStartEvent()
    {
        return _gameStartEvent;
    }

    public UnityEvent GetGameEndEvent()
    {
        return _gameEndEvent;
    }

    public UnityEvent GetBallActiveEvent()
    {
        return _ballActivatedEvent;
    }

    public UnityEvent GetBallDeactiveEvent()
    {
        return _ballDeactivatedEvent;
    }
}
