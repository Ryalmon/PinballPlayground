
using System.Collections.Generic;
using UnityEngine;

public class FlipperManager : MonoBehaviour
{
    [SerializeField] List<Flippers> _leftFlippers = new List<Flippers>();
    [SerializeField] List<Flippers> _rightFlippers = new List<Flippers>();


    [SerializeField] Sprite unpressedButton;
    [SerializeField] Sprite pressedButton;

    [SerializeField] SpriteRenderer leftButton;
    [SerializeField] SpriteRenderer rightButton;


    private void Start()
    {
        AssignEvents();
    }

    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetGameEndEvent().AddListener(DeactivateAllFlippers);
    }

    public void AddToList(Flippers newFlipper, List<Flippers> flipList)
    {
        flipList.Add(newFlipper);
    }

    public void ActivateLeftFlippers()
    {
        print("Activate Left Flippers");
        GameplayManagers.Instance.UI.LeftFlipperButtonPressed();
        //Goes through the list of left flippers and activates them
        foreach(Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.Flip();
        }

        leftButton.sprite = pressedButton;
    }

    public void DeactivateLeftFlippers()
    {
        GameplayManagers.Instance.UI.LeftFlipperButtonPassive();
        //Goes through the list of left flippers and activates them
        foreach (Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.UnFlip();
        }

        leftButton.sprite = unpressedButton;
    }

    public void ActivateRightFlippers()
    {
        GameplayManagers.Instance.UI.RightFlipperButtonPressed();
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.Flip();
        }

        rightButton.sprite = pressedButton;
    }

    public void DeactivateRightFlippers()
    {
        GameplayManagers.Instance.UI.RightFlipperButtonPassive();
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.UnFlip();
        }

        rightButton.sprite = unpressedButton;
    }

    private void DeactivateAllFlippers()
    {
        DeactivateLeftFlippers();
        DeactivateRightFlippers();
    }

    public void ResetLists()
    {
        _leftFlippers.Clear();
        _rightFlippers.Clear();
    }
}
