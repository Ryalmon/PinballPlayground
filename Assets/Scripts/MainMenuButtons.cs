using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    private const int _gameplayScene = 1;

    public void PlayGame()
    {
        _playButton.interactable = false;
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

    public void GoToMainMenu()
    {
        //_playButton in this case is the button that goes to the main menu.
        //Both buttons are never in the same scene. That's why this is fine. Maybe
        _playButton.interactable = false;
        UniversalManager.Instance.Scene.LoadScene(0);
    }

}
