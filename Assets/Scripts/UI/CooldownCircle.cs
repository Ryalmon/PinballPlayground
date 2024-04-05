using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownCircle : MonoBehaviour
{
    public void Activate()
    {
        if (GameplayManagers.Instance.State.GPS == GameStateManager.GamePlayState.Intro)
            SubscribeToStartEvent();
        else
            StartCooldown();
    }

    private void SubscribeToStartEvent()
    {
        GameplayManagers.Instance.State.GetGameStartEvent().AddListener(StartCooldown);
    }

    private void StartCooldown()
    {
        GetComponent<Animator>().SetTrigger("StartAnim");
    }
}
