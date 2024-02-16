using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] TMP_Text _scoreMultiplierText;
    private float _scoreMultiplierStartingFontSize;
    [Space]
    [SerializeField] Vector2 _scoreTextLocation;
    [Space]

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

    public void UpdateScoreUI(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }

    public void UpdateTimerUI(float time)
    {
        time = Mathf.Round(time * 10) * .1f;
        _timerText.text = time.ToString();
    }

    public void UpdateMultiplierUI(float multiplier)
    {
        //Updates the score multiplier UI text
        multiplier = MathF.Round(multiplier * 10f) / 10f;
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

        if (UniversalManager.Instance.Save.ValidScoreInput(GameplayParent.Instance.Score.CurrentScore))
        {
            DisplayKeyboard();
        }
        else
        {
            GameplayParent.Instance.State.EndScene();
        }
    }

    private void DisplayFinalScore()
    {
        _finalScoreText.text = GameplayParent.Instance.Score.CurrentScore.ToString();
        _finalScoreDisplay.SetActive(true);
    }

    private void DisplayKeyboard()
    {
        Debug.Log("DisplayKeyboard");
        _inputKeyboardDisplay.SetActive(true);
    }

    public Vector2 GetScoreTextLocation()
    {
        return _scoreTextLocation;
    }
}
