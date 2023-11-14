using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public static SceneLoadingManager M_Instance;

    void Awake()
    {
        EstablishSingleton();
    }

    private void EstablishSingleton()
    {
        if (M_Instance != null && M_Instance != this)
        {
            Destroy(gameObject);
        }

        M_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int CurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
