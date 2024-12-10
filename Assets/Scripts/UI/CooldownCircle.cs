using UnityEngine;

public class CooldownCircle : MonoBehaviour
{
    private Animator _cooldownAnimator;
    
    private void Start()
    {
        _cooldownAnimator = GetComponent<Animator>();
    }

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
        _cooldownAnimator.SetTrigger("StartAnim");
    }
}
