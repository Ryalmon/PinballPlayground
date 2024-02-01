using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab;

    internal static GamePlayState GPS;

    public enum GamePlayState { 
        Intro,
        Play,
        End
    };


    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Testing Spawn Ball, will remove later
        //Quinn note - spawning balls this way will break the respawning system
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnBall();
        }
    }

    public void EndGameState()
    {

    }

    public void SpawnBall()
    {
        //Places a ball
        Instantiate(BallPrefab, Vector2.zero, Quaternion.identity);

        //---Currently the ball is placed at (0,0) this will change later!---
    }
}
