using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NameInput : MonoBehaviour
{
    [Header("Variables")]
    public string Name;
    [Space]

    [Header("References")]
    [SerializeField] TMP_Text _nameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddLetter()
    {
        Name += EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        ChangeText();
    }

    private void ChangeText()
    {
        _nameText.text = Name;
    }


}
