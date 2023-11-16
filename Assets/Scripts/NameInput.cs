using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NameInput : MonoBehaviour
{
    [Header("Variables")]
    public string Name;
    [SerializeField] int _allowedNameLength;
    private bool _buttonInteractionStatus;
    [Space]

    [Header("References")]
    [SerializeField] TMP_Text _nameText;
    [SerializeField] List<Button> _textButtons; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddLetter()
    {
        Name += EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        CheckInteractableStatus();
        ChangeText();
    }

    public void Backspace()
    {
        if (Name.Length-1 < 0)
            return;
        Name = Name.Remove(Name.Length-1);
        CheckInteractableStatus();
        ChangeText();
    }

    private void CheckInteractableStatus()
    {
        if (Name.Length >= _allowedNameLength)
        {
            Interactable(false);
            return;
        }
        if (!_buttonInteractionStatus)
            Interactable(true);
    }

    private void Interactable(bool canInteract)
    {
        _buttonInteractionStatus = canInteract;
        foreach(Button currentButton in _textButtons)
        {
            Debug.Log("ChangeStatus" + canInteract);
            currentButton.interactable = canInteract;
        }
    }

    private void ChangeText()
    {
        _nameText.text = Name;
    }


}
