using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab;

    internal GamePlayState GPS;

    public enum GamePlayState { 
        Intro,
        Play,
        End
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Testing Spawn Ball, will remove later
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnBall();
        }
    }

    public void SpawnBall()
    {
        Instantiate(BallPrefab, Vector2.zero, Quaternion.identity);
    }
}
