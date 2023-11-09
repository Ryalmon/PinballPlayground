using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager M_Instance;

    void Awake()
    {
        if (EstablishSingleton())
            return;
    }

    private bool EstablishSingleton()
    {
        if (M_Instance != null && M_Instance != this)
        {
            Destroy(gameObject);
            return false;
        }

        M_Instance = this;
        DontDestroyOnLoad(gameObject);
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
