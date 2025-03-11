using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    private const int _mainMenuScene = 0;
    private const int _gameplayScene = 1;
    private const int _creditsScene = 2;

    public void PlayGame()
    {
        UniversalManager.Instance.Save.ResetMostRecentScore();
        UniversalManager.Instance.Scene.LoadScene(_gameplayScene);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void ViewCredits()
    {
        UniversalManager.Instance.Scene.LoadScene(_creditsScene);
    }
}
