using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
//using System;

public class GameUIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [Header("Text")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] TMP_Text _scoreMultiplierText;
    [Space]

    [Header("TextData")]
    [SerializeField] Vector2 _scoreTextLocation;
    [SerializeField] float _roundTo2DigitsAt;
    [SerializeField] float _scoreMultiplierScalingRate;
    [SerializeField] private Gradient _gradient;
    private float _scoreMultiplierStartingFontSize;
    private string _roundScoreTo = "F1";
    [Space]

    [Header("ScorePopup")]
    [SerializeField] GameObject _scorePopUpSpawnSource;
    [SerializeField] GameObject _scorePopUpObject;
    [SerializeField] Vector2 _scorePopupLocation;
    [SerializeField] float _scorePopupTime;
    [SerializeField] float _scorePopupYVariability;
    [SerializeField] float _scorePopupRate;
    [SerializeField] float _scorePopupRateScaler;
    Queue<float> _scorePopupQueue = new Queue<float>();
    private Coroutine _scorePopupCoroutine;
    [Space]

    [Header("Buttons")]
    [SerializeField] private GameObject _ballLaunchButton;
    [SerializeField] private GameObject _leftFlipperButton;
    [SerializeField] private GameObject _rightFlipperButton;
    [SerializeField] private Sprite _flipperButtonPassive;
    [SerializeField] private Sprite _flipperButtonPressed;
    [Space]

    [Header("Visuals")]
    [SerializeField] private GameObject _placementRegion;
    [SerializeField] private float _placementRegionFadeInTime;
    [SerializeField] private float _placementRegionFadeOutTime;
    private Coroutine _placementRegionCoroutine;

    [Header("Game End")]
    [SerializeField] GameObject _finalScoreDisplay;
    [SerializeField] TMP_Text _finalScoreText;
    [Space]
    [SerializeField] GameObject _inputKeyboardDisplay;

    [Space]
    [SerializeField] float _finalScoreWaitTime;


    // Start is called before the first frame update
    void Start()
    {
        AssignEvents();
        _scoreMultiplierStartingFontSize = _scoreMultiplierText.fontSize;
    }

    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(GameEndUI);
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(BallLaunchButtonPressed);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(BallLaunchButtonPressed);
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(ResetMultiplier);
        GameplayManagers.Instance.State.GetBallDeactiveEvent().AddListener(SetLaunchButtonActive);
    }

    public void UpdateScoreUI(int currentScore, int newScore)
    {
        UpdateScoreBoard(currentScore);
        CreateScorePopUp(newScore);
    }

    private void UpdateScoreBoard(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }

    public void UpdateTimerUI(float time)
    {
        if (time < _roundTo2DigitsAt) _roundScoreTo = "F2";
        //time = Mathf.Round(time * 10) * .1f;
        _timerText.text = time.ToString(_roundScoreTo);
    }

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
    }

    public void GameEndUI()
    {
        StartCoroutine(GameEndUIProcess());
    }

    private IEnumerator GameEndUIProcess()
    {
        _leftFlipperButton.SetActive(false);
        _rightFlipperButton.SetActive(false);

        DisplayFinalScore();
        yield return new WaitForSeconds(_finalScoreWaitTime);
        _finalScoreDisplay.SetActive(false);

        if (UniversalManager.Instance.Save.ValidScoreInput(GameplayManagers.Instance.Score.CurrentScore))
        {
            DisplayKeyboard();
        }
        else
        {
            GameplayManagers.Instance.State.EndScene();
        }
    }

    public void CreateScorePopUp(float scorePopUp)
    {
        _scorePopupQueue.Enqueue(scorePopUp);
        if (_scorePopupCoroutine == null)
            _scorePopupCoroutine = StartCoroutine(PopupCreationProcess());
        
    }

    private IEnumerator PopupCreationProcess()
    {
        //This code is gibberish to read I will comment it later - Ryan
        while(_scorePopupQueue.Count > 0)
        {
            Vector2 popupLoc = new Vector2(_scorePopupLocation.x, Random.Range(_scorePopupLocation.y - _scorePopupYVariability,
            _scorePopupLocation.y + _scorePopupYVariability));
            GameObject textPopup = Instantiate(_scorePopUpObject, popupLoc, _scorePopUpObject.transform.rotation);
            textPopup.GetComponent<TMP_Text>().text = _scorePopupQueue.Dequeue().ToString();
            textPopup.transform.SetParent(_scorePopUpSpawnSource.transform);
            Destroy(textPopup.gameObject, _scorePopupTime);
            yield return new WaitForSeconds(_scorePopupRate / (1 +(_scorePopupQueue.Count * _scorePopupRateScaler)));
        }
        _scorePopupCoroutine = null;
    }

    public void SetLaunchButtonActive()
    {
        if (GameplayManagers.Instance.State.GPS != GameStateManager.GamePlayState.Play) 
        return;
        _ballLaunchButton.SetActive(true);
    }

    public void BallLaunchButtonPressed()
    {
        _ballLaunchButton.SetActive(false);
    }

    #region FlipperButtons
    public void LeftFlipperButtonPressed()
    {
        _leftFlipperButton.GetComponent<Image>().sprite = _flipperButtonPressed;
    }

    public void LeftFlipperButtonPassive()
    {
        _leftFlipperButton.GetComponent<Image>().sprite = _flipperButtonPassive;
    }

    public void RightFlipperButtonPressed()
    {
        _rightFlipperButton.GetComponent<Image>().sprite = _flipperButtonPressed;
    }

    public void RightFlipperButtonPassive()
    {
        _rightFlipperButton.GetComponent<Image>().sprite = _flipperButtonPassive;
    }
    #endregion

    #region ItemPlacementZone
    public void ShowPlacementRegion()
    {
        if (_placementRegionCoroutine != null)
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

    private void DisplayFinalScore()
    {
        _finalScoreText.text = GameplayManagers.Instance.Score.CurrentScore.ToString();
        _finalScoreDisplay.SetActive(true);
    }

    private void DisplayKeyboard()
    {
        //Debug.Log("DisplayKeyboard");
        _inputKeyboardDisplay.SetActive(true);
    }

    public Vector2 GetScoreTextLocation()
    {
        return _scoreTextLocation;
    }
}
