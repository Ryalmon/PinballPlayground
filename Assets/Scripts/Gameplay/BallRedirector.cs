using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRedirector : MonoBehaviour
{
    [SerializeField] Vector2 _redirectDirection;
    [SerializeField] float _redirectForce;
    [SerializeField] float _xVariance;
    [SerializeField] float _holdTime;
    [SerializeField] float _scoreMultiplier = 1;

    [SerializeField] private float _regrabImmunity;

    private Queue<BallPhysics> balls = new Queue<BallPhysics>();
    private List<BallPhysics> _immuneBalls = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics ballPhysics = collision.gameObject.GetComponent<BallPhysics>();
        if (ballPhysics != null && !balls.Contains(ballPhysics) && !_immuneBalls.Contains(ballPhysics))
        {
            StartCoroutine(RedirectProcess(ballPhysics));
        }
    }

    private void BallEntrance(BallPhysics newBall)
    {
        balls.Enqueue(newBall);
        newBall.transform.position = transform.position;

        newBall.ResetVelocity();
        newBall.PhysicsEnabled(false);

        UniversalManager.Instance.Sound.PlaySFX("SlingRing");

        GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.Redirector, _scoreMultiplier);
    }

    private void BallFire()
    {
        BallPhysics ballReleased = balls.Dequeue();
        _immuneBalls.Add(ballReleased);
        Vector2 currentFireForce = new(Random.Range(_redirectDirection.x-_xVariance,_redirectDirection.x +_xVariance)
            ,_redirectDirection.y);

        ballReleased.PhysicsEnabled(true);
        ballReleased.OverrideBallForce(currentFireForce * _redirectForce);

        UniversalManager.Instance.Sound.PlaySFX("BallLaunch");
    }

    private void EnableRegrab(BallPhysics newBall)
    {
        _immuneBalls.Remove(newBall);
    }

    private IEnumerator RedirectProcess(BallPhysics newBall)
    {
        BallEntrance(newBall);
        
        yield return new WaitForSeconds(_holdTime);

        BallFire();

        yield return new WaitForSeconds(_regrabImmunity);

        EnableRegrab(newBall);
    }
}
