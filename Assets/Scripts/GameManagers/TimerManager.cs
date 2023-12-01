using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float TimeRemaining;

    IEnumerator CountDown()
    {
        //Count the timer down until it reaches 0
        while(TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            yield return null;
        }
        //Activate anything that needs to happen after the timer reaches 0
        EndTimer();
    }

    void EndTimer()
    {
        
    }
}
