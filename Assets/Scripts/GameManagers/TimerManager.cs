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

    private IEnumerator CountDown()
    {
        //Count the timer down until it reaches 0
        while(TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            GameplayManagers.Instance.UI.UpdateTimerUI(TimeRemaining);
            yield return null;
        }
        //Activate anything that needs to happen after the timer reaches 0
        EndTimer();
    }

    void EndTimer()
    {
        GameplayManagers.Instance.State.EndGameState();
    }
}
