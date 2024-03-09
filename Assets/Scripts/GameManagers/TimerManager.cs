using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float TimeRemaining;
    private Coroutine _countDownCoroutine;

    public void Start()
    {
        AssignEvents();
    }

    /// <summary>
    /// Subscribes to all necessary Events
    /// </summary>
    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetGameStartEvent().AddListener(StartCountdown);
    }

    public void StartCountdown()
    {
        _countDownCoroutine = StartCoroutine(CountDown());
    }

    public void StopCountDown()
    {
        StopCoroutine(_countDownCoroutine);
    }

    /// <summary>
    /// Counts down the timer
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDown()
    {
        //Count the timer down until it reaches 0
        while(TimeRemaining > 0)
        {
            //Decreases the timer float variable
            TimeRemaining -= Time.deltaTime;
            //Updates the ui to display the float
            GameplayManagers.Instance.UI.UpdateTimerUI(TimeRemaining);
            yield return null;
        }
        //Display 0 on the timer
        GameplayManagers.Instance.UI.UpdateTimerUI(0);
        //Activate anything that needs to happen after the timer reaches 0
        EndTimer();
    }

    /// <summary>
    /// This function is called when the timer has concluded
    /// </summary>
    void EndTimer()
    {
        GameplayManagers.Instance.State.EndGameState();
    }
}
