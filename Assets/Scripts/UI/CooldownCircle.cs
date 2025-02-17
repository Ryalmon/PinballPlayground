using UnityEngine;

public class CooldownCircle : MonoBehaviour
{
    [SerializeField] float yPercent;
    [SerializeField] float xPercent;
    [SerializeField] RectTransform gameplayRect;
    [SerializeField] GameObject linkedButton;

    private Animator _cooldownAnimator;
    
    private void Start()
    {
        _cooldownAnimator = GetComponent<Animator>();

        SetPositionToButton();
        // Worldspace to ui space
        //GetComponent<RectTransform>().position = renderCamera.InverseTransformPoint(linkedButton.transform.position);
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

    private void SetPositionToButton()
    {
        Vector2 ViewportPos = Camera.main.WorldToViewportPoint(linkedButton.transform.position);
        // canvas is 800 x 600 px. at least its scale with screen size i guess
        GetComponent<RectTransform>().anchoredPosition = new Vector2(800 * ViewportPos.x, 600 * ViewportPos.y);
    }

    private void OnDrawGizmosSelected()
    {
        SetPositionToButton();
    }

}
