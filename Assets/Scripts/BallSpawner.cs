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
    //private bool canLaunchBall;
    [SerializeField] private GameObject BallLaunchButton;
    [SerializeField] private GameObject _ballShooter;
    [SerializeField] private GameObject _ballSpawner;
    [SerializeField] private float _launchPower;


    public void BallSplit(Vector3 CurrentLocation, GameObject Splitter)
    {
        Destroy(Splitter);
        Instantiate(BallPrefab, CurrentLocation, Quaternion.identity);
    }

    public void LaunchBall()
    {
        GameObject Ball = Instantiate(BallPrefab, _ballShooter.transform.position, Quaternion.identity);
        //Determines direction and multiplies that by the launch power
        Ball.GetComponent<BallPhysics>().OverrideBallForce( _launchPower * _ballShooter.GetComponent<BallShooter>().ShootBallDir());
        Debug.Log("Ball has been fired");
    }

    public void CheckBallCountIsZero()
    {
        if (BallsInScene.Count <= 0)
            BallCountIsZero();
    }

    private void BallCountIsZero()
    {
        GameplayManagers.Instance.Score.StopScaling();
        SetButtonActive();
        //makes the launcher visible
        _ballSpawner.SetActive(true);
    }

    public void RemoveBall(GameObject ball)
    {
        BallsInScene.Remove(ball.GetComponent<BallPhysics>());
        CheckBallCountIsZero();
        Destroy(ball.gameObject);
        Debug.Log("Ball has been removed");
    }


    public void SetButtonActive()
    {
        BallLaunchButton.SetActive(true);

    }

    public void SpawnBallButtonPressed()
    {
        BallLaunchButton.SetActive(false);
        GameplayManagers.Instance.Score.StartScaling();
        //makes the launcher invisible
        _ballSpawner.SetActive(false);
    }

    public int GetBallsInSceneCount()
    {
        return BallsInScene.Count;
    }

}
