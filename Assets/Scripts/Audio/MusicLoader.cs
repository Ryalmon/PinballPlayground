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
            StartCoroutine(MainMenuMusic());
        }
        else if (sceneName == "NEWSwansonGameplay")
        {
            StartCoroutine(GameMusic());
        }
    }

    IEnumerator MainMenuMusic()
    {
        float progress = 0.5f;
        while (progress > 0f)
        {
            progress -= Time.deltaTime;
            UniversalManager.Instance.Sound.musicSource.volume = progress;
        }
        UniversalManager.Instance.Sound.StopMusic("GameMusic");
        UniversalManager.Instance.Sound.PlayMusic("MenuMusic");
        while (progress < 0.5f)
        {
            progress += Time.deltaTime;
            UniversalManager.Instance.Sound.musicSource.volume = progress;
        }
        yield return new WaitForSeconds(0f);
    }

    IEnumerator GameMusic()
    {
        float progress = 0.5f;
        while (progress > 0f)
        {
            progress -= Time.deltaTime;
            UniversalManager.Instance.Sound.musicSource.volume = progress;
        }
        UniversalManager.Instance.Sound.StopMusic("MenuMusic");
        UniversalManager.Instance.Sound.PlayMusic("GameMusic");
        while (progress < 0.5f)
        {
            progress += Time.deltaTime;
            UniversalManager.Instance.Sound.musicSource.volume = progress;
        }
        yield return new WaitForSeconds(0f);
    }
}
