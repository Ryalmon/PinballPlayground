using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
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
    }

    private void DisplayFinalScore()
    {
        _finalScoreText.text = GameplayParent.Instance.Score.CurrentScore.ToString();
        _finalScoreDisplay.SetActive(true);
    }

    private void DisplayKeyboard()
    {
        _inputKeyboardDisplay.SetActive(true);
    }
}
