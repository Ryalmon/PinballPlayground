using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float _gravityForce;
    [SerializeField] int _baseScorePerTick;
    [SerializeField] float _scoreTickRate;
    private List<BallPhysics> _objectsInRadius = new List<BallPhysics>();
    private Coroutine _moveObjectsCoroutine;
    private Coroutine _addScoreCoroutine;

    IEnumerator MovePinballs()
    {
        while(_objectsInRadius.Count > 0)
        {
            foreach (BallPhysics bp in _objectsInRadius)
            {
                bp.ApplyForceToBall(CalculateGravityForce(bp));
            }
            yield return null;
        }
        _moveObjectsCoroutine = null;
    }

    IEnumerator GenerateScore()
    {
        while(_objectsInRadius.Count > 0)
        {
            GameplayParent.Instance.Score.CreatePointParticles(gameObject, _baseScorePerTick * _objectsInRadius.Count);
            yield return new WaitForSeconds(_scoreTickRate);
        }
        _addScoreCoroutine = null;
    }

    Vector2 CalculateGravityForce(BallPhysics bp)
    {
        Vector2 newForce = (transform.position - bp.gameObject.transform.position ) * _gravityForce * Time.deltaTime;
        return newForce;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics bp = collision.gameObject.GetComponent<BallPhysics>();
        if (bp != null)
        {
            _objectsInRadius.Add(bp);

            StartActiveCoroutines();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BallPhysics bp = collision.gameObject.GetComponent<BallPhysics>();
        if (bp != null)
        {
            _objectsInRadius.Remove(bp);
        }
    }

    private void StartActiveCoroutines()
    {
        if(_moveObjectsCoroutine == null && _addScoreCoroutine == null)
        {
            _moveObjectsCoroutine = StartCoroutine(MovePinballs());
            _addScoreCoroutine = StartCoroutine(GenerateScore());
        }
    }
}
