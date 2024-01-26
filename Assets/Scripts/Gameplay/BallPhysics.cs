using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ApplyForceToBall(Vector2 newForce)
    {
        rb.AddForce(newForce);
    }

    public void RedirectBall(Vector2 newDirection)
    {
        rb.velocity = newDirection * rb.velocity.magnitude;
    }

    public void OverrideBallForce(Vector2 newForce)
    {
        rb.velocity = newForce;
    }
}
