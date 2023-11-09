using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float TimeRemaining;

    IEnumerator CountDown()
    {
        while(TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            yield return null;
        }
    }
}
