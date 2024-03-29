using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRedirector : MonoBehaviour
{
    [SerializeField] Vector2 _redirectDirection;
    [SerializeField] float _redirectForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics ballphysics = collision.gameObject.GetComponent<BallPhysics>();
        if (ballphysics != null)
        {
            ballphysics.OverrideBallForce(_redirectDirection * _redirectForce);
        }
    }
}
