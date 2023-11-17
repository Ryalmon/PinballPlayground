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

    private void Awake()
    {
        //ChangeText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeText();
        }
    }

    private void ChangeText()
    {
        _nameText.text = SaveManager.M_Instance.ReturnPlayerName(_scorePosition);
        _scoreText.text = SaveManager.M_Instance.ReturnPlayerScore(_scorePosition).ToString();
    }
}
