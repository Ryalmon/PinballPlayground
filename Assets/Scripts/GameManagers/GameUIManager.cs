using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
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
}
