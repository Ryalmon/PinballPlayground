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
    [SerializeField] Button _enterButton;
    [SerializeField] NameBlacklistSO _blacklist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddLetter()
    {
        //Adds the letter from the button into the currently inputted name
        Name += EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        CheckInteractableStatus();
        ChangeText();
    }

    public void Backspace()
    {
        //Checks that the name isn't empty
        if (Name.Length-1 < 0)
            return;
        //Removes the last letter in the name
        Name = Name.Remove(Name.Length-1);
        CheckInteractableStatus();
        ChangeText();
    }

    public void SubmitName()
    {
        UniversalManager.Instance.Save.PlaceScoreInArray(Name,GameplayManagers.Instance.Score.CurrentScore, UniversalManager.Instance.Save.ReturnArrayLength()-1);
        GameplayManagers.Instance.State.EndScene();
    }

    private void CheckInteractableStatus()
    {
        //Checks if the name is at the name length limit
        if (Name.Length >= _allowedNameLength)
        {
            //Makes all input buttons non interactable
            Interactable(false);
            CheckNameBlacklist();
            return;
        }
        if (!_buttonInteractionStatus)
            //Makes all input buttons interactable
            Interactable(true);
    }

    private void Interactable(bool canInteract)
    {
        _buttonInteractionStatus = canInteract;
        //Goes through each button and changes the interactability of them
        foreach (Button currentButton in _textButtons)
        {
            currentButton.interactable = canInteract;
        }
        _enterButton.interactable = !canInteract;
        
    }

    private void ChangeText()
    {
        //Displays the name in the game world
        _nameText.text = Name;
    }

    private void CheckNameBlacklist()
    {
        foreach (string names in _blacklist._blackListedNames)
            if (Name == names)
                _enterButton.interactable = false;
            
    }


}
