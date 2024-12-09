using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _otherButton;
    [SerializeField] private Button _viewCreditsButton;
    private const int _mainMenuScene = 0;
    private const int _gameplayScene = 1;
    private const int _creditsScene = 2;

    private void Start()
    {
        if(_otherButton == null)
        {
            _otherButton = GetComponent<Button>();
        }

    }
    public void PlayGame()
    {
        _playButton.interactable = false;
        _otherButton = _viewCreditsButton.GetComponent<Button>();
        _otherButton.interactable = false;
        UniversalManager.Instance.Save.ResetMostRecentScore();
        UniversalManager.Instance.Scene.LoadScene(_gameplayScene);
    }

    public static void QuitGame()
    {

        Application.Quit();
        /*if (Application.isEditor)
        {
         //   UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }*/

    }

    public void ViewCredits()
    {
        _viewCreditsButton.interactable = false;
        _otherButton = _playButton.GetComponent<Button>();
        _otherButton.interactable = false;
        UniversalManager.Instance.Scene.LoadScene(_creditsScene);
    }
}
