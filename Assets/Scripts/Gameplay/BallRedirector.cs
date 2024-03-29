using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRedirector : MonoBehaviour
{
    [SerializeField] Vector2 _redirectDirection;
    [SerializeField] float _redirectForce;
    private GameObject ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics ballphysics = collision.gameObject.GetComponent<BallPhysics>();
        if (ballphysics != null)
        {
            ball = collision.gameObject;
            StartCoroutine(RedirectBall());
        }
    }

    private IEnumerator RedirectBall()
    {
        ball.transform.position = transform.position;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        
        yield return new WaitForSeconds(0.5f);
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ball.GetComponent<BallPhysics>().OverrideBallForce(_redirectDirection * _redirectForce);
        

    }
}
