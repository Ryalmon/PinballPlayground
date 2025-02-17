using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
//using System;

/// <summary>
/// This script is an afront to god. I'm sorry that this was every created. This will forever live as my greatest mistake. - Ryan 12/9/24
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [Header("Text")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] Slider _timerSlider;
    [SerializeField] TMP_Text _scoreMultiplierText;
    [Space]

    [Header("TextData")]
    [SerializeField] Vector2 _scoreTextLocation;
    [SerializeField] float _scoreMultiplierScalingRate;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Gradient _timeLeftGradient;
    [SerializeField] private Animator _multiplierAnimation;
    private float _scoreMultiplierStartingFontSize;
    
    [Space]

    [Header("Timer")]
    [SerializeField] float _roundTo1DigitsAt;
    [SerializeField] float _roundTo2DigitsAt;
    [SerializeField] float _startCountDownAnimAt;
    [SerializeField] private Animator _timerAlertAnim;
    private string _roundScoreTo = "F0";
    private UnityEvent<float> _timerChecks = new UnityEvent<float>();
    [Space]

    [Header("ScorePopup")]
    [SerializeField] float _scorePopupTime;
    [SerializeField] private float _popupSizeScalar;
    [SerializeField] private float _maxPopupSize;
    [SerializeField] private AnimationCurve _popupSizeCurve;

    [Space]
    [SerializeField] private Transform _popupSizeScale;
    [SerializeField] private TMP_Text _scorePopupText;
    [SerializeField] private Animator _scorePopupAnimator;
    private float _currentScorePopupValue = 0;

    private Coroutine _scorePopupCoroutine;

    private const string POPUP_PLAY_TRIGGER = "PlayPopUp";
    [Space]

    [Header("Buttons")]
    [SerializeField] private GameObject _ballLaunchButton;
    [SerializeField] private Animator _launchAnimator;
    [SerializeField] private Animator _tutorialButtonAnimator;
    [SerializeField] private SpriteRenderer _leftFlipperButton;
    [SerializeField] private SpriteRenderer _rightFlipperButton;
    [SerializeField] private Sprite _flipperButtonPassive;
    [SerializeField] private Sprite _flipperButtonPressed;
    [Space]

    [Header("Visuals")]
    [Header("PlacementRegion")]
    [SerializeField] private GameObject _placementRegion;
    [SerializeField] private float _placementRegionFadeInTime;
    [SerializeField] private float _placementRegionFadeOutTime;
    private Coroutine _placementRegionCoroutine;
    [Header("PlaceableCooldown")]
    [SerializeField] private CooldownCircle _leftCooldown;
    [SerializeField] private CooldownCircle _rightCooldown;
    [Header("Countdown")]
    [SerializeField] private Animator _countdownAnim;
    [Header("Milestone")]
    [SerializeField] int _milestoneIncrement;
    [SerializeField] Animator _milestoneAnimation;
    private float _currentMilestoneGoal = 0;
    [Space]
    [Header("Game Hints")]
    [SerializeField] private Animator _placeablesHints;
    [Space]

    [Header("Game End")]
    [SerializeField] GameObject _finalScoreDisplay;
    [SerializeField] TMP_Text _finalScoreText;
    [Space]
    [SerializeField] GameObject _CongratsGameEndDisplay;
    [SerializeField] GameObject _KeyboardDisplay;
    [SerializeField] GameObject _Fireworks;
    [SerializeField] Animator _fw1;
    [SerializeField] Animator _fw2;

    [Space]
    [SerializeField] float _startTimeForEnd;
    [SerializeField] float _waitTimeAfterScore;
    [SerializeField] float _finalScoreWaitTime;


    // Start is called before the first frame update
    void Start()
    {
        AssignEvents();
        _scoreMultiplierStartingFontSize = _scoreMultiplierText.fontSize;
        _currentMilestoneGoal = _milestoneIncrement;
    }

    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(GameEndUI);
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(BallLaunchButtonPressed);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(BallLaunchButtonPressed);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(ResetMultiplier);
        GameplayManagers.Instance.State.GetBallDeactiveEvent().AddListener(SetLaunchButtonActive);
        GameplayManagers.Instance.State.GetGameStartEvent().AddListener(HideTutorialButton);

        _timerChecks.AddListener(OneDigitRound);
    }

    public void UpdateScoreUI(int currentScore, int newScore)
    {
        UpdateScoreBoard(currentScore);
        CheckForMilestoneHit(currentScore);
        CreateScorePopUp(newScore);
    }

    private void UpdateScoreBoard(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }

    private void CheckForMilestoneHit(int currentScore)
    {
        if(currentScore >= _currentMilestoneGoal)
        {
            _currentMilestoneGoal += _milestoneIncrement;
            TriggerMilestoneAnimation();
        }
    }

    private void TriggerMilestoneAnimation()
    {
        _milestoneAnimation.SetTrigger("ActivateMilestoneEffect");
        UniversalManager.Instance.Sound.PlaySFX("ScoreMilestone");
    }

    #region TimerUI
    public void UpdateTimerUI(float time)
    {
        _timerChecks?.Invoke(time);
        //time = Mathf.Round(time * 10) * .1f;
        _timerText.text = time.ToString(_roundScoreTo);
        _timerSlider.value = time;
        ColorBlock c = _timerSlider.colors;
        c.disabledColor = _timeLeftGradient.Evaluate(1-(time / 120));
        c.normalColor = _timeLeftGradient.Evaluate(1-(time / 120));
        _timerSlider.colors = c;
    }



    private void OneDigitRound(float time)
    {
        if (time < _roundTo1DigitsAt)
        {
            _roundScoreTo = "F1";
            _timerChecks.AddListener(TwoDigitRound);
            _timerChecks.RemoveListener(OneDigitRound);
        }
    }

    private void TwoDigitRound(float time)
    {
        if (time < _roundTo2DigitsAt)
        {
            _roundScoreTo = "F2";
            TimerAlertAnimActive(true);
            _timerChecks.AddListener(CheckStartCountDownAnim);
            _timerChecks.RemoveListener(TwoDigitRound);
        }
    }

    private void CheckStartCountDownAnim(float time)
    {
        if (time < _startCountDownAnimAt)
        {
            //StartCoroutine(CountdownTextChange());
            _countdownAnim.SetTrigger("StartCountDown");
            _timerChecks.RemoveListener(CheckStartCountDownAnim);
        }
    }

    private void TimerAlertAnimActive(bool active)
    {
        _timerAlertAnim.SetBool("AlertActive", active);
    }
    #endregion

    public void UpdateMultiplierUI(float multiplier)
    {
        //Updates the score multiplier UI text
        UpdateMultiplierText(multiplier);
        //Updates the score multiplier UI size
        //_scoreMultiplierText.fontSize = _scoreMultiplierStartingFontSize * multiplier;
        UpdateMultiplierSize(_scoreMultiplierText.fontSize + _scoreMultiplierScalingRate);

        //Updates the score multiplier UI color
        UpdateMultiplierColor(multiplier); 
    }

    private void UpdateMultiplierText(float multiplier)
    {
        _scoreMultiplierText.text = multiplier.ToString("F1") + "x";
    }

    private void UpdateMultiplierSize(float newSize)
    {
        _scoreMultiplierText.fontSize = newSize;
    }

    public void UpdateMultiplierColor(float multiplier)
    {
        float colorGradientAmount = multiplier / (GameplayManagers.Instance.Score.GetStartingMultiplier() * GameplayManagers.Instance.Score.GetMaxMultiplier())
            - (GameplayManagers.Instance.Score.GetStartingMultiplier() / GameplayManagers.Instance.Score.GetMaxMultiplier());

        _scoreMultiplierText.color = _gradient.Evaluate(colorGradientAmount);
    }

    public void ResetMultiplier()
    {
        UpdateMultiplierText(GameplayManagers.Instance.Score.GetStartingMultiplier());
        UpdateMultiplierSize(_scoreMultiplierStartingFontSize);
        UpdateMultiplierColor(GameplayManagers.Instance.Score.GetStartingMultiplier());
        UpdateMultiplierAnimation(false);
    }

    public void UpdateMultiplierAnimation(bool active)
    {
        _multiplierAnimation.SetBool("Shake", active);
    }

    public void GameEndUI()
    {
        TimerAlertAnimActive(false);
        StartCoroutine(GameEndUIProcess());
    }

    private IEnumerator GameEndUIProcess()
    {
        _leftFlipperButton.gameObject.SetActive(false);
        _rightFlipperButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(_startTimeForEnd);
        DisplayFinalScore();
        yield return new WaitForSeconds(_waitTimeAfterScore);

        if (UniversalManager.Instance.Save.ValidScoreInput(GameplayManagers.Instance.Score.CurrentScore))
        {
            DisplayCongrats();
            DisplayLeaderboardGameEnd();
        }
        else
        {
            yield return new WaitForSeconds(_finalScoreWaitTime);
            GameplayManagers.Instance.State.EndScene();
        }
    }

    public void CreateScorePopUp(float scorePopUp)
    {
        _scorePopupAnimator.SetTrigger(POPUP_PLAY_TRIGGER);

        UpdateScorePopUp(scorePopUp);
        DeterminePopUpScale();

        if (_scorePopupCoroutine != null)
        {
            StopCoroutine(_scorePopupCoroutine);
        }
        _scorePopupCoroutine = StartCoroutine(PopUpProcess());
    }

    private void UpdateScorePopUp(float scorePopUp)
    {
        _currentScorePopupValue += scorePopUp;
        _scorePopupText.text = _currentScorePopupValue.ToString();
    }

    private void DeterminePopUpScale()
    {
        float newPopupSize = 1 + (_popupSizeCurve.Evaluate(_currentScorePopupValue / _maxPopupSize)*_popupSizeScalar);
        _popupSizeScale.localScale = new Vector2(newPopupSize, newPopupSize);
    }

    private IEnumerator PopUpProcess()
    {
        yield return new WaitForSeconds(_scorePopupTime);
        _currentScorePopupValue = 0;
    }

    public void SetLaunchButtonActive()
    {
        if (GameplayManagers.Instance.State.GPS != GameStateManager.GamePlayState.Play) return;

        _launchAnimator.SetBool("ButtonVisible", true);
    }

    public void BallLaunchButtonPressed()
    {
        _launchAnimator.SetBool("ButtonVisible", false);
    }

    /// <summary>
    /// I hate this script with every fiber of my being but I might as well stay consistent
    /// </summary>
    public void HideTutorialButton()
    {
        _tutorialButtonAnimator.SetBool("ButtonVisible", false);
    }

    #region FlipperButtons
    public void LeftFlipperButtonPressed()
    {
        _leftFlipperButton.sprite = _flipperButtonPressed;
        //_leftFlipperButton.GetComponent<Image>().sprite = _flipperButtonPressed;
    }

    public void LeftFlipperButtonPassive()
    {
        _leftFlipperButton.sprite = _flipperButtonPassive;
        //_leftFlipperButton.GetComponent<Image>().sprite = _flipperButtonPassive;
    }

    public void RightFlipperButtonPressed()
    {
        _rightFlipperButton.sprite = _flipperButtonPressed;
        //_rightFlipperButton.GetComponent<Image>().sprite = _flipperButtonPressed;
    }

    public void RightFlipperButtonPassive()
    {
        _rightFlipperButton.sprite = _flipperButtonPassive;
        //_rightFlipperButton.GetComponent<Image>().sprite = _flipperButtonPassive;
    }
    #endregion

    #region ItemPlacementZone
    public void ShowPlacementRegion()
    {
        if (_placementRegionCoroutine != null|| GameplayManagers.Instance.State.GPS == GameStateManager.GamePlayState.End)
            return;

        UnityEvent postFadeIn = new UnityEvent();
        postFadeIn.AddListener(StartRegionProcess);

        _placementRegionCoroutine = GameplayManagers.Instance.Fade.FadeGameObjectIn(_placementRegion,_placementRegionFadeInTime,postFadeIn);
    }

    private void StartRegionProcess()
    {
        StartCoroutine(ShowPlacementRegionProcess());
    }

    private IEnumerator ShowPlacementRegionProcess()
    {
        while (GameplayManagers.Instance.Placement.AreItemsBeingDragged() && _placementRegionCoroutine != null)
            yield return null;
        HidePlacementRegion();
    }

    public void HidePlacementRegion()
    {
        UnityEvent postFadeOut = new UnityEvent();
        postFadeOut.AddListener(EndOfRegionVisuals);
        _placementRegionCoroutine = GameplayManagers.Instance.Fade.FadeGameObjectOut(_placementRegion, _placementRegionFadeOutTime, postFadeOut);
        
    }

    private void EndOfRegionVisuals()
    {
        _placementRegionCoroutine = null;
        CheckRestartPlacementRegionVisuals();
    }

    private void CheckRestartPlacementRegionVisuals()
    {
        if (GameplayManagers.Instance.Placement.AreItemsBeingDragged())
            ShowPlacementRegion();
    }
    #endregion

    #region Cooldown Circles
    public void ActivateLeftCooldownCircle()
    {
        _leftCooldown.Activate();
    }

    public void ActivateRightCooldownCircle()
    {
        _rightCooldown.Activate();
    }
    #endregion

    #region Hints
    public void ShowPlaceableHints()
    {
        _placeablesHints.SetTrigger("Show");
    }
    #endregion

    private void DisplayFinalScore()
    {
        _finalScoreText.text = GameplayManagers.Instance.Score.CurrentScore.ToString();
        _finalScoreDisplay.SetActive(true);
    }

    private void DisplayCongrats()
    {
        _CongratsGameEndDisplay.SetActive(true);

    }

    private void DisplayLeaderboardGameEnd()
    {
        //Debug.Log("DisplayKeyboard");
        _KeyboardDisplay.SetActive(true);

        StartCoroutine(DisplayFireWorks());
    }

    private IEnumerator DisplayFireWorks()
    {
        yield return new WaitForSeconds(1);
        _Fireworks.SetActive(true);
        _fw1.gameObject.SetActive(true);
        _fw1.SetTrigger("StartFirework");
        
        yield return new WaitForSeconds(.5f);
        _fw2.gameObject.SetActive(true);
        _fw2.SetTrigger("StartFirework");
        
    }

    public Vector2 GetScoreTextLocation()
    {
        return _scoreTextLocation;
    }
}
