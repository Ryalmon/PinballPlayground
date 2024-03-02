using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour, IPlaceable
{
    [Header("Variables")]
    [SerializeField] float _baseGravityForce;
    [SerializeField] float _xForceMultiplier;
    [SerializeField] float _yForceMultiplier;
    [SerializeField] float _upwardsForceMultiplier;
    [SerializeField] float _ballSpeedForceInfluence;
    [Space]
    [SerializeField] float _scoreTickRate;
    [Space]
    [SerializeField] float _destroyTime;

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
            for(int i = 0; i < _objectsInRadius.Count;i++)
                GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.BlackHole);
            yield return new WaitForSeconds(_scoreTickRate);
        }
        _addScoreCoroutine = null;
    }

    Vector2 CalculateGravityForce(BallPhysics bp)
    {
        Vector2 newForce = (transform.position - bp.gameObject.transform.position).normalized
            * (transform.position - bp.gameObject.transform.position).sqrMagnitude
            * _baseGravityForce
            * (bp.GetComponent<Rigidbody2D>().velocity.magnitude / _ballSpeedForceInfluence) *
            Time.deltaTime;
        newForce *= new Vector2(_xForceMultiplier, _yForceMultiplier);
        if (newForce.y > 0 && bp.GetComponent<Rigidbody2D>().velocity.y < 0)
            newForce *= new Vector2(1, _upwardsForceMultiplier);
        /*else
            if (bp.gameObject.transform.position.y < transform.position.y)
                newForce += new Vector2(0, 10000f * Time.deltaTime);*/
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

    public void Placed()
    {
        GetComponent<Drift>().enabled = true;
        GetComponent<BlackHole>().enabled = true;
    }

    public void DestroyPlacedObject()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectOut(gameObject, _destroyTime);
        Destroy(gameObject,_destroyTime);
    }

}
