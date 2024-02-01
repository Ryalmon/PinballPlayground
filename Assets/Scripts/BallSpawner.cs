using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallPrefab;
    public Vector2 BallSpawnLocation;
    public bool BallIsInGame;
    public bool MultiBallIsActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Spawns a ball when there is not one in game. 
    void Update()
    {
        if (BallIsInGame == false)
        {
            StartSpawnBall();
        }
    }

    public void StartSpawnBall()
    {
        GameObject Ball;
        Ball = Instantiate(BallPrefab, BallSpawnLocation, Quaternion.identity);
        BallIsInGame = true;
    }

}
