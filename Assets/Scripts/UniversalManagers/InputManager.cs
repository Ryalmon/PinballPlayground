using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager M_Instance;
    public TouchInput TI;

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

    /*void SetupInput()
    {
        TI = new TouchInput();
        TI.Controls.TestTouch.started += ctx => StartTouch(ctx);
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log(TI.Controls.TestTouch.ReadValue<Vector2>());
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
