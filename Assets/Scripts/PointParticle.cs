using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticle : MonoBehaviour
{
    [SerializeField] float awayTime;
    [SerializeField] float endTime;

    internal Vector2 AwayDirection;
    internal Vector2 EndingLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignStartValues(Vector2 away, Vector2 end)
    {
        AwayDirection = (Vector2)transform.position + away;
        EndingLocation = end;
        StartCoroutine(MoveAway());
    }

    IEnumerator MoveAway()
    {
        float movePercent = 0;
        Vector2 startPos = transform.position;
        while(movePercent < 1)
        {
            movePercent += Time.deltaTime/awayTime;
            transform.position = Vector2.Lerp(startPos, AwayDirection, Mathf.Sqrt(movePercent));
            yield return null;
        }
    }

    public IEnumerator MoveTowards()
    {
        float movePercent = 0;
        Vector2 startPos = transform.position;
        while (movePercent < 1)
        {
            movePercent += Time.deltaTime/endTime;
            transform.position = Vector2.Lerp(startPos, EndingLocation, movePercent);
            yield return null;
        }
        Destroy(gameObject);
    }

}

