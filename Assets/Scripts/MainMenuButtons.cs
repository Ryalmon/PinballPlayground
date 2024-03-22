using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    private const int _gameplayScene = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        _playButton.interactable = false;
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

}
