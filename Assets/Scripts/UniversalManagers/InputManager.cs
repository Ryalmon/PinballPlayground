using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    public PlayerInputActions PIA;

    void Start()
    {
        EstablishInput();
    }

    #region DebugButtons
    private void CloseGame(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void ResetScores(InputAction.CallbackContext obj)
    {
        UniversalManager.Instance.Save.ResetSaveData();
    }

    private void ToMainMenu(InputAction.CallbackContext obj)
    {
        UniversalManager.Instance.Scene.LoadScene(0);
    }
    private void RefreshScene(InputAction.CallbackContext obj)
    {
        UniversalManager.Instance.Scene.ReloadScene();
    }

    private void DestroyBalls(InputAction.CallbackContext obj)
    {
        if (GameplayManagers.Instance != null)
            GameplayManagers.Instance.Ball.RemoveAllBalls();
    }

    private void DisableEvent(InputAction.CallbackContext obj)
    {
        InputSystemUIInputModule iSUIIM = FindObjectOfType<InputSystemUIInputModule>();
        if (iSUIIM != null)
            iSUIIM.enabled = !iSUIIM.enabled;
    }

    #endregion

    private void EstablishInput()
    {
        PIA = new PlayerInputActions();
        PIA.DebugButtons.Enable();

        PIA.DebugButtons.Escape.started += CloseGame;
        PIA.DebugButtons.ResetScore.started += ResetScores;
        PIA.DebugButtons.LoadMainMenu.started += ToMainMenu;
        PIA.DebugButtons.ReloadCurrentScene.started += RefreshScene;
        PIA.DebugButtons.DestroyBalls.started += DestroyBalls;
        PIA.DebugButtons.DisableEventSystem.started += DisableEvent;

    }

    private void OnDestroy()
    {
        PIA.DebugButtons.Escape.started -= CloseGame;
        PIA.DebugButtons.ResetScore.started -= ResetScores;
        PIA.DebugButtons.LoadMainMenu.started -= ToMainMenu;
        PIA.DebugButtons.ReloadCurrentScene.started -= RefreshScene;
        PIA.DebugButtons.DestroyBalls.started -= DestroyBalls;
        PIA.DebugButtons.DisableEventSystem.started -= DisableEvent;

        PIA.DebugButtons.Disable();
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

}
