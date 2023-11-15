using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NameInput : MonoBehaviour
{
    public string Name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddLetter()
    {
        
        Name += EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
    }


}
