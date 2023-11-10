using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager M_Instance;
    void Awake()
    {
        if (EstablishSingleton())
            return;
        SetupInput();
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

    void SetupInput()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
