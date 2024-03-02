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
    //[SerializeField] private GameObject _ballShooterParent;
    //private float launchDirection;
    [SerializeField] private float _launchPower;
    [SerializeField] private float _ballRemovalTime;


    public void BallSplit(Vector3 CurrentLocation, GameObject Splitter)
    {
        Destroy(Splitter);
        Instantiate(BallPrefab, CurrentLocation, Quaternion.identity);
    }

    public void LaunchBall()
    {
        //BallSpawnLocation = _ballShooter.transform.position;
        //GameObject Ball;
        GameObject Ball = Instantiate(BallPrefab, _ballShooter.transform.position, Quaternion.identity);


        /*//BallTransform = Ball.transform;
        //_ballShooter.transform.position.z 
        Debug.Log(_ballShooterParent.transform.rotation.z);

        float fRotation = _ballShooterParent.transform.rotation.z * Mathf.Deg2Rad;
        float fX = Mathf.Sin(fRotation);
        float fY = Mathf.Cos(fRotation);
        Vector2 v2 = new Vector2(fY, fX);
        Debug.Log(v2);
        //Debug.Log(fY);
        //Debug.Log(fX);
        Vector2 BallShootAngle = new Vector2(fY, fX);
        
        /// Negative or positive direction modifier for the balls' direction
        if (fY > 0)
        {
            launchDirection = -1;
        }
        else 
        {
            launchDirection = 1;
        }
        //BallShootAngle = _ballShooter.transform.forward;

        

        Ball.GetComponent<BallPhysics>().OverrideBallForce(BallShootAngle * _launchPower * launchDirection);*/

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
        GameplayManagers.Instance.Score.StopScaling();
        SetButtonActive();
    }

    public void RemoveBall(GameObject ball)
    {
        BallsInScene.Remove(ball.GetComponent<BallPhysics>());
        CheckBallCountIsZero();
        GameplayManagers.Instance.Fade.FadeGameObjectOut(ball, _ballRemovalTime);
        Destroy(ball.gameObject,_ballRemovalTime);
    }


    public void SetButtonActive()
    {
        BallLaunchButton.SetActive(true);
    }

    public void SpawnBallButtonPressed()
    {
        BallLaunchButton.SetActive(false);
        GameplayManagers.Instance.Score.StartScaling();
    }

    public int GetBallsInSceneCount()
    {
        return BallsInScene.Count;
    }

}
