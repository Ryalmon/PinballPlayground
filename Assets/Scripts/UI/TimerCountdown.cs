using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    [SerializeField] TMP_Text _timerCountdownText;

    public void SetTime(string newText)
    {
        _timerCountdownText.text = newText;
    }
}
