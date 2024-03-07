using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float _launchPower;
    [SerializeField] private float _ballRemovalTime;
    public GameObject BallPrefab;
    public List<BallPhysics> BallsInScene;
    //private bool canLaunchBall;
    [SerializeField] private GameObject BallLaunchButton;
    [SerializeField] private GameObject _ballShooter;
    [SerializeField] private GameObject _ballSpawner;

    private void Start()
    {
        AssignEvents();
    }
    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(LaunchBall);
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(RemoveAllBalls);
    }

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
    }

    public void CheckBallCountIsZero()
    {
        if (BallsInScene.Count <= 0)
            BallCountIsZero();
    }

    private void BallCountIsZero()
    {
        //GameplayManagers.Instance.Score.StopScaling();
        GameplayManagers.Instance.State.DeactivateBallState();
        //makes the launcher visible
        _ballSpawner.SetActive(true);
    }

    public void RemoveBall(GameObject ball)
    {
        BallsInScene.Remove(ball.GetComponent<BallPhysics>());
        CheckBallCountIsZero();
        GameplayManagers.Instance.Fade.FadeGameObjectOut(ball, _ballRemovalTime, null);
        Destroy(ball.gameObject, _ballRemovalTime);
    }

    public void RemoveAllBalls()
    {
        while (BallsInScene.Count > 0)
        {
            RemoveBall(BallsInScene[0].gameObject);
        }
    }

    public int GetBallsInSceneCount()
    {
        return BallsInScene.Count;
    }

}
