using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float TimeRemaining;

    IEnumerator CountDown()
    {
        while(TimeRemaining > 0)
        {
            yield return null;
        }
    }
}
