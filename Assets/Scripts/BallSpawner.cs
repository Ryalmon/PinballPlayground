using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallPrefab;
    public Vector2 BallSpawnLocation;
    public bool MultiBallIsActive;
    public List<BallPhysics> BallsInScene;
    public Transform BallTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Spawns a ball when there is not one in game. 
    void Update()
    {
        if (BallsInScene.Count <= 0)
        {
            StartSpawnBall();
        }

    }
    /// <summary>
    /// Spawns the ball at the top of the screen
    /// when there are no balls in play
    /// </summary>
    public void StartSpawnBall()
    {
        GameObject Ball;

        Ball = Instantiate(BallPrefab, BallSpawnLocation, Quaternion.identity);
        BallTransform = Ball.transform;
    }
    public void BallSplit(Vector3 CurrentLocation, GameObject Splitter)
    {
        Destroy(Splitter);
        Instantiate(BallPrefab, CurrentLocation, Quaternion.identity);
        
    }

}
