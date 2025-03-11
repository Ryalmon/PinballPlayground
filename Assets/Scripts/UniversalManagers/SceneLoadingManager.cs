using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoadingManager : MonoBehaviour
{
    private UnityEvent _preSceneChange = new UnityEvent();
    private UnityEvent _postSceneChange = new UnityEvent();
    private int _previousScene = -1;
    public int CurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int index)
    {
        StartCoroutine(SceneLoadDelay(index));
    }

    public void ReloadScene()
    {
        LoadScene(CurrentScene());
    }

    public IEnumerator SceneLoadDelay(int index)
    {
        _preSceneChange?.Invoke();
        _previousScene = SceneManager.GetActiveScene().buildIndex;
        SceneTransition st = FindObjectOfType<SceneTransition>();
        if (st != null)
            st.SceneTransitionIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
        _postSceneChange?.Invoke();
    }

    public UnityEvent GetPreSceneLoadEvent() => _preSceneChange;

    public UnityEvent PostSceneChangeEvent()
    {
        return _postSceneChange;
    }

    public int PreviousScene()
    {
        return _previousScene;
    }

    public bool IsSameAsPreviousScene()
    {
        return SceneManager.GetActiveScene().buildIndex == _previousScene;
    }
}
