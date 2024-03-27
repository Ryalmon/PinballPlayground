using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardText : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] int _scorePosition;
    [Space]
    [Header("References")]
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _scoreText;

    private void Start()
    {
        ChangeText();
        CheckForRecentScore();
    }

    private void Update()
    {
        //StartCoroutine(TextUpdate()); 
    }

    public void ChangeText()
    {
        //Gets the name and score from JSON at a certain position and displays it on the scoreboard
        _nameText.text = UniversalManager.Instance.Save.ReturnPlayerName(_scorePosition);
        _scoreText.text = UniversalManager.Instance.Save.ReturnPlayerScore(_scorePosition).ToString();
    }

    private void CheckForRecentScore()
    {
        if (UniversalManager.Instance.Save.ReturnRecentScorePos() == _scorePosition)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Highlight");
            //Debug.Log("highlight");
        }
    }
    public IEnumerator TextUpdate()
    {
        yield return new WaitForSecondsRealtime(10f);
        ChangeText();
    }
}
