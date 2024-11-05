using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticle : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float _awayTime;
    [SerializeField] float _endTime;
    [SerializeField] float _awayAcceleration;

    internal Vector2 AwayDirection;
    internal Vector2 EndingLocation;
    int _baseValue;

    private Coroutine _moveAwayCoroutine;
    private Coroutine _moveTowardsCoroutine;

    // Start is called before the first frame update

    public void SetPointValue(int value)
    {
        _baseValue = value;
    }

    public void StartMoveAway(Vector3 away)
    {
        StopMoveAway();
        _moveAwayCoroutine = StartCoroutine(MoveAway(away));
    }

    public void StopMoveAway()
    {
        if(_moveAwayCoroutine != null)
        {
            StopCoroutine(_moveAwayCoroutine);
        }
    }

    public IEnumerator MoveAway(Vector2 away)
    {
        //Assigns the direction that the projectile is launched out at the start
        AwayDirection = (Vector2)transform.position + away;

        float movePercent = 0;
        Vector2 startPos = transform.position;
        //Launches the particle away from where it is spawned
        while(movePercent < 1)
        {
            movePercent += Time.deltaTime/_awayTime;
            transform.position = Vector2.Lerp(startPos, AwayDirection, Mathf.Sqrt(movePercent));
            yield return null;
        }
    }

    public void StartMoveTowards(Vector2 end)
    {
        StopMoveTowards();
        _moveTowardsCoroutine = StartCoroutine(MoveTowards(end));
    }

    public void StopMoveTowards()
    {
        if(_moveTowardsCoroutine != null)
        {
            StopCoroutine(_moveTowardsCoroutine);
        }
    }

    public IEnumerator MoveTowards(Vector2 end)
    {
        //Assigns where the projectile flies to at the end
        EndingLocation = end;

        float movePercent = 0;
        float speedMult = 1;
        Vector2 startPos = transform.position;
        //Launches the particle towards the ending location
        while (movePercent < 1)
        {
            speedMult += Time.deltaTime * _awayAcceleration;
            movePercent += (Time.deltaTime/_endTime) * speedMult;
            transform.position = Vector2.Lerp(startPos, EndingLocation, movePercent);
            yield return null;
        }
        GameplayManagers.Instance.Score.AddToScore(_baseValue);
        gameObject.SetActive(false);
    }

}

