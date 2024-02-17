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

    public void CheckBallCountIsZero()
    {
        if (BallsInScene.Count <= 0)
            BallCountIsZero();
    }

    private void BallCountIsZero()
    {
        GameplayParent.Instance.Score.StopScaling();
        SetButtonActive();
    }

    public void RemoveBall(GameObject ball)
    {
        BallsInScene.Remove(ball.GetComponent<BallPhysics>());
        CheckBallCountIsZero();
        Destroy(ball.gameObject);
    }


    public void SetButtonActive()
    {
        BallLaunchButton.SetActive(true);
    }

    public void SpawnBallButtonPressed()
    {
        BallLaunchButton.SetActive(false);
        GameplayParent.Instance.Score.StartScaling();
    }

    public int GetBallsInSceneCount()
    {
        return BallsInScene.Count;
    }

}
