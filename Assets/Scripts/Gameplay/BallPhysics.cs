using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject BallController;

    // Start is called before the first frame update
    void Start()
    {
        BallController = GameObject.FindGameObjectWithTag("BallController");
        BallController.GetComponent<BallSpawner>().BallsInScene.Add(this);
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

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    public void SetPosition(Vector2 newPos)
    {
        transform.position = newPos;
    }

    public void SetParent(GameObject newParent)
    {
        transform.parent = newParent.transform;
    }
    public void RemoveParent()
    {
        transform.parent = null;
    }

    public void PhysicsEnabled(bool enabled)
    {
        if(enabled)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            return;
        }
        rb.bodyType = RigidbodyType2D.Static;
    }
}
