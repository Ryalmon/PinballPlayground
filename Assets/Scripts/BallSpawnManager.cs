using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float _launchPower;
    [SerializeField] private float _ballRemovalTime;

    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject _ballShooter;

    private List<BallPhysics> BallsInScene = new List<BallPhysics>();

    private void Start()
    {
        AssignEvents();
    }
    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(LaunchBall);
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(RemoveAllBalls);
    }

    public void LaunchBall()
    {
        //Creates the ball
        GameObject Ball = CreateBall(_ballShooter.GetComponent<BallShooter>().GetBallShootPoint());
        //Adds the ball to the list of balls in scene
        AddBall(Ball);

        //Determines direction and multiplies that by the launch power
        Ball.GetComponent<BallPhysics>().OverrideBallForce( _launchPower * _ballShooter.GetComponent<BallShooter>().ShootBallDir());
    }

    public GameObject CreateBall(Vector2 spawnPos)
    {
        return Instantiate(BallPrefab, spawnPos, Quaternion.identity);
    }

    public void CheckBallCountIsZero()
    {
        if (BallsInScene.Count <= 0)
            BallCountIsZero();
    }

    private void BallCountIsZero()
    {
        //Make game enter the deactivate ball state
        GameplayManagers.Instance.State.DeactivateBallState();
    }

    public void AddBall(GameObject ball)
    {
        BallsInScene.Add(ball.GetComponent<BallPhysics>());
    }

    public void RemoveBall(GameObject ball)
    {
        BallsInScene.Remove(ball.GetComponent<BallPhysics>());
        CheckBallCountIsZero();
        GameplayManagers.Instance.Fade.FadeGameObjectOut(ball, _ballRemovalTime, null);
        GameplayManagers.Instance.Fade.StartTrailFadeOut(ball, _ballRemovalTime/3);
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
