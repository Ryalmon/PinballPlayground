using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRedirector : MonoBehaviour
{
    [SerializeField] Vector2 _redirectDirection;
    [SerializeField] float _redirectForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics bp = collision.gameObject.GetComponent<BallPhysics>();
        if (bp != null)
        {
            //bp.RedirectBall(_redirectDirection);
            bp.OverrideBallForce(_redirectDirection * _redirectForce);
        }
    }
}
