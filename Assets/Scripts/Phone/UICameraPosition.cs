using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraPosition : MonoBehaviour
{
    [SerializeField] Camera uiCamera;
    [SerializeField] Camera gameObjectCamera;

    // Start is called before the first frame update
    void Start()
    {
        uiCamera.aspect = gameObjectCamera.aspect;

    }

    private void OnDrawGizmos()
    {
        uiCamera.aspect = gameObjectCamera.aspect;

    }
}
