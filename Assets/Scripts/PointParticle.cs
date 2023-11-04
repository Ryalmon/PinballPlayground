using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticle : MonoBehaviour
{
    public Vector2 AwayDirection;
    public Vector2 EndingLocation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAway());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveAway()
    {
        float movePercent = 0;
        while(movePercent < 1)
        {
            movePercent += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveTowards()
    {
        float movePercent = 0;
        while(movePercent < 0)
        {
            movePercent += Time.deltaTime;
            yield return null;
        }
    }
}
