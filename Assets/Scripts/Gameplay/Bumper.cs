using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour, IPlaceable
{
    [SerializeField] float _forceMultiplier;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null )
        {
            Debug.DrawRay(collision.contacts[collision.contactCount-1].point, collision.gameObject.transform.position - (Vector3)collision.contacts[collision.contactCount-1].point, Color.green,5);

            collision.gameObject.GetComponent<BallPhysics>().OverrideBallForce(DetermineShootDirection(collision));
            GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.Bumper);
        }
    }

    /*private Vector2 DetermineShootDirection(Vector2 otherObject)
    {
        return (otherObject - new Vector2(transform.position.x, transform.position.y)).normalized * _forceMultiplier;
    }*/

    private Vector2 DetermineShootDirection(Collision2D collision)
    {
        return (collision.gameObject.transform.position - (Vector3)collision.contacts[collision.contactCount - 1].point).normalized * _forceMultiplier;
    }

    public void Placed()
    {
        GetComponent<Drift>().enabled = true;
    }

    public void DestroyPlacedObject()
    {
        Destroy(gameObject);
    }

    public void NotPlaced()
    {
        GetComponent<Bumper>().enabled = false;
    }
}
