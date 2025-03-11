using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    public PlayerInputActions PIA;
    private EventSystem _eventSystem;

    void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
        EstablishInput();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        UniversalManager.Instance.Scene.GetPreSceneLoadEvent().AddListener(delegate { EventSystemEnable(false); });
        UniversalManager.Instance.Scene.PostSceneChangeEvent().AddListener(delegate { EventSystemEnable(true); });
    }

    private void EventSystemEnable(bool enabled)
    {
        _eventSystem.enabled = enabled;
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

    private void TimeAccel(InputAction.CallbackContext obj)
    {
        Time.timeScale += 1;
    }

    private void TimeReset(InputAction.CallbackContext obj)
    {
        Time.timeScale = 1;
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

        PIA.DebugButtons.SpeedAccel.started += TimeAccel;
        PIA.DebugButtons.SpeedReset.started += TimeReset;

    }

    private void OnDestroy()
    {
        PIA.DebugButtons.Escape.started -= CloseGame;
        PIA.DebugButtons.ResetScore.started -= ResetScores;
        PIA.DebugButtons.LoadMainMenu.started -= ToMainMenu;
        PIA.DebugButtons.ReloadCurrentScene.started -= RefreshScene;
        PIA.DebugButtons.DestroyBalls.started -= DestroyBalls;
        PIA.DebugButtons.DisableEventSystem.started -= DisableEvent;

        PIA.DebugButtons.SpeedAccel.started -= TimeAccel;
        PIA.DebugButtons.SpeedReset.started -= TimeReset;

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
