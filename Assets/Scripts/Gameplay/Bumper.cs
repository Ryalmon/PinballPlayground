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
            collision.gameObject.GetComponent<BallPhysics>().OverrideBallForce(DetermineShootDirection(collision.transform.position));
            GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.Bumper);
        }
    }

    private Vector2 DetermineShootDirection(Vector2 otherObject)
    {
        return (otherObject - new Vector2(transform.position.x, transform.position.y)).normalized * _forceMultiplier;
    }

    public void Placed()
    {
        GetComponent<Drift>().enabled = true;
    }

    public void DestroyPlacedObject()
    {
        Destroy(gameObject);
    }
}
