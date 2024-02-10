using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float _forceMultiplier;
    [SerializeField] int _baseScoreValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null )
        {
            collision.gameObject.GetComponent<BallPhysics>().OverrideBallForce(DetermineShootDirection(collision.transform.position));
            GameplayParent.Instance.Score.CreatePointParticles(gameObject, _baseScoreValue);
        }
    }

    private Vector2 DetermineShootDirection(Vector2 otherObject)
    {
        return (otherObject - new Vector2(transform.position.x, transform.position.y)).normalized * _forceMultiplier;
    }
}
