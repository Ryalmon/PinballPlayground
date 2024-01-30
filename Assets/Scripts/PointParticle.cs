using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticle : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float _awayTime;
    [SerializeField] float _endTime;
    [SerializeField] float _awayAcceleration;
    //Change to internal later
    public int _pointValue;

    internal Vector2 AwayDirection;
    internal Vector2 EndingLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignStartValues(Vector2 away, Vector2 end)
    {
        //Assigns the direction that the projectile is launched out at the start
        AwayDirection = (Vector2)transform.position + away;
        //Assigns where the projectile flies to at the end
        EndingLocation = end;
        StartCoroutine(MoveAway());
    }

    IEnumerator MoveAway()
    {
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

    public IEnumerator MoveTowards()
    {
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
        GameplayParent.Instance.Score.AddToScore(_pointValue);
        Destroy(gameObject);
    }

}

