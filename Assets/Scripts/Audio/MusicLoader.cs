using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DetermineSong();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        UniversalManager.Instance.Scene.PostSceneChangeEvent().AddListener(DetermineSong);
    }

    private void UnsubscribeToEvents()
    {
        UniversalManager.Instance.Scene.PostSceneChangeEvent().RemoveListener(DetermineSong);
    }

    private void DetermineSong()
    {
        // Create a temporary reference to the current scene.
        int sceneID = SceneManager.GetActiveScene().buildIndex;

        if (sceneID == 0)
        {
            if (!UniversalManager.Instance.Scene.IsSameAsPreviousScene() && UniversalManager.Instance.Scene.PreviousScene() != 2)
                StartCoroutine(MainMenuMusic());
        }
        else if (sceneID == 1)
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
