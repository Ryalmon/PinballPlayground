using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButtons : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void GoToMainMenu()
    {
        _button.interactable = false;
        UniversalManager.Instance.Scene.LoadScene(0);
    }
}
