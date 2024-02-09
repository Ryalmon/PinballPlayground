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
    private bool canLaunchBall;
    [SerializeField] private GameObject BallLaunchButton;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Spawns a ball when there is not one in game. 
    void Update()
    {
 
    }

    public void BallSplit(Vector3 CurrentLocation, GameObject Splitter)
    {
        Destroy(Splitter);
        Instantiate(BallPrefab, CurrentLocation, Quaternion.identity);

    }

    public void LaunchBall()
    {
        GameObject Ball;
        Ball = Instantiate(BallPrefab, BallSpawnLocation, Quaternion.identity);
        BallTransform = Ball.transform;
    }

    public void SetButtonActive()
    {
        BallLaunchButton.SetActive (true);
    }

    public void SetButtonInactive()
    {
        BallLaunchButton.SetActive(false);
    }

}
