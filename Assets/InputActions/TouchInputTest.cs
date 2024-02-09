using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputTest : MonoBehaviour
{
    public PlayerInput PlayerInputInstance;

    private InputAction testTouch;
 
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputInstance = GetComponent<PlayerInput>();
        PlayerInputInstance.currentActionMap.Enable();

        testTouch = PlayerInputInstance.currentActionMap.FindAction("TestTouch2");

        testTouch.performed += TestTouch2_performed;
        testTouch.canceled += TestTouch2_canceled;
    }

    private void TestTouch2_performed(InputAction.CallbackContext context)
    {
        Debug.Log("Player Tapped");

      
    }

    private void TestTouch2_canceled(InputAction.CallbackContext context)
    {
        Debug.Log("Item Dropped");

        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
