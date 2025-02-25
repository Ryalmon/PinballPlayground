using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSaveButton : MonoBehaviour
{
    /// <summary>
    /// Called on button press
    /// </summary>
    public void ResetSave()
    {
        // Resets the save data
        UniversalManager.Instance.Save.ResetSaveData();
        // Reloads the scene
        UniversalManager.Instance.Scene.ReloadScene();
    }
}
