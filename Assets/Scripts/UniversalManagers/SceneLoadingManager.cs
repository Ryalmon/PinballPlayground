using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public int CurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int index)
    {
        StartCoroutine(SceneLoadDelay(index));
    }

    public IEnumerator SceneLoadDelay(int index)
    {
        SceneTransition st = FindObjectOfType<SceneTransition>();
        if (st != null)
            st.SceneTransitionIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }
}
