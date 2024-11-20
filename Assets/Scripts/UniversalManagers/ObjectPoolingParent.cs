using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingParent : MonoBehaviour
{
    public static ObjectPoolingParent Instance;

    private bool _gameplayPoolCreated;

    /// <summary>
    /// Establishes the instances
    /// </summary>
    public void SetupInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PoolCreated()
    {
        _gameplayPoolCreated = true;
    }

    /// <summary>
    /// Adds a specific child
    /// </summary>
    /// <param name="newChild"> The child to add</param>
    public void AddObjectAsChild(GameObject newChild)
    {
        newChild.transform.SetParent(transform);
    }

    /// <summary>
    /// Removes a specific child
    /// </summary>
    /// <param name="child"> The child to remove </param>
    public void RemoveObjectAsChild(GameObject child)
    {
        child.transform.SetParent(null);
    }

    #region
    public bool GetGameplayPoolCreated() => _gameplayPoolCreated;
    #endregion
}

