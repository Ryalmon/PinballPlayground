using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    public void TriggerTickSFX()
    {
        UniversalManager.Instance.Sound.PlaySFX("TimerTick");
    }
}
