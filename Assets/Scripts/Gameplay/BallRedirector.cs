using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRedirector : MonoBehaviour
{
    [SerializeField] Vector2 _redirectDirection;
    [SerializeField] float _redirectForce;
    [SerializeField] float _xVariance;
    [SerializeField] float _holdTime;
    private Queue<GameObject> balls = new Queue<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics ballphysics = collision.gameObject.GetComponent<BallPhysics>();
        if (ballphysics != null)
        {
            StartCoroutine(RedirectProcess(collision.gameObject));
        }
    }

    private void BallEntrance(GameObject newBall)
    {
        balls.Enqueue(newBall);
        newBall.transform.position = transform.position;
        newBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        newBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        UniversalManager.Instance.Sound.PlaySFX("SlingRing");
    }

    private void BallFire()
    {
        GameObject ballReleased = balls.Dequeue();
        Vector2 currentFireForce = new(Random.Range(_redirectDirection.x-_xVariance,_redirectDirection.x +_xVariance)
            ,_redirectDirection.y);
        ballReleased.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ballReleased.GetComponent<BallPhysics>().OverrideBallForce(currentFireForce * _redirectForce);
        UniversalManager.Instance.Sound.PlaySFX("BallLaunch");
    }

    private IEnumerator RedirectProcess(GameObject newBall)
    {
        BallEntrance(newBall);
        
        yield return new WaitForSeconds(_holdTime);

        BallFire();
    }
}
