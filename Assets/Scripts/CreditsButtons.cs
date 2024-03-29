using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButtons : MonoBehaviour
{
    [SerializeField] private Button _viewCreditsButton;
    [SerializeField] private const int _mainMenuScene = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewCredits()
    {
        _viewCreditsButton.interactable = false;
        UniversalManager.Instance.Scene.LoadScene(_mainMenuScene);
    }
}
