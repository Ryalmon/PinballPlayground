using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            UniversalManager.Instance.Sound.StopMusic("GameMusic");
            UniversalManager.Instance.Sound.PlayMusic("MenuMusic");
        }
        else if (sceneName == "NEWSwansonGameplay")
        {
            UniversalManager.Instance.Sound.StopMusic("MenuMusic");
            UniversalManager.Instance.Sound.PlayMusic("GameMusic");
        }
    }
}
