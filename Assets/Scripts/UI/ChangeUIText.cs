using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeUIText : MonoBehaviour
{
    [SerializeField] private TMP_Text _textToChange;

    [SerializeField] private string _beforeText;
    [SerializeField] private string _afterText;

    private bool _textToggle = false;

    public void ChangeText()
    {
        _textToggle = !_textToggle;

        if(_textToggle)
        {
            _textToChange.text = _afterText;
        }
        else
        {
            _textToChange.text = _beforeText;
        }
    }
}
