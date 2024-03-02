using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System;

public class GameUIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] TMP_Text _scoreMultiplierText;
    private float _scoreMultiplierStartingFontSize;
    [Space]
    [SerializeField] Vector2 _scoreTextLocation;
    [SerializeField] GameObject _scorePopUpSpawnSource;
    [SerializeField] GameObject _scorePopUpObject;
    [SerializeField] float _scorePopupTime;
    [SerializeField] Vector2 _scorePopupLocation;
    [SerializeField] float _scorePopupYVariability;
    [SerializeField] float _scorePopupRate;
    [SerializeField] float _scorePopupRateScaler;
    Queue<float> _scorePopupQueue = new Queue<float>();
    private Coroutine _scorePopupCoroutine;
    [Space]
    [SerializeField] GameObject leftFlipperButton;
    [SerializeField] GameObject rightFlipperButton;
    [SerializeField] Sprite _buttonPassive;
    [SerializeField] Sprite _buttonPressed;

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
        _scoreMultiplierStartingFontSize = _scoreMultiplierText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {

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
        time = Mathf.Round(time * 10) * .1f;
        _timerText.text = time.ToString("F1");
    }

    public void UpdateMultiplierUI(float multiplier)
    {
        //Updates the score multiplier UI text
        multiplier = Mathf.Round(multiplier * 10f) / 10f;
        _scoreMultiplierText.text = multiplier.ToString() + "x";
        //Updates the score multiplier UI size
        _scoreMultiplierText.fontSize = _scoreMultiplierStartingFontSize * multiplier;
    }

    public void GameEndUI()
    {
        StartCoroutine(GameEndUIProcess());
    }

    private IEnumerator GameEndUIProcess()
    {
        DisplayFinalScore();
        yield return new WaitForSeconds(_finalScoreWaitTime);
        _finalScoreDisplay.SetActive(false);

        leftFlipperButton.SetActive(false);
        rightFlipperButton.SetActive(false);

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

    /*private IEnumerator ScorePopUpProcess(GameObject popUp)
    {
        yield return new WaitForSeconds(_scorePopupTime);
        Destroy(popUp.gameObject);
    }*/

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
