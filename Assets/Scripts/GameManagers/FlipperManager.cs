using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipperManager : MonoBehaviour
{
    [SerializeField] List<Flippers> _leftFlippers = new List<Flippers>();
    [SerializeField] List<Flippers> _rightFlippers = new List<Flippers>();


    public void AddToList(Flippers newFlipper, List<Flippers> flipList)
    {
        flipList.Add(newFlipper);
    }

    public void ActivateLeftFlippers()
    {
        GameplayManagers.Instance.UI.LeftFlipperButtonPressed();
        //Goes through the list of left flippers and activates them
        foreach(Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.Flip();
        }
    }

    public void DeactivateLeftFlippers()
    {
        GameplayManagers.Instance.UI.LeftFlipperButtonPassive();
        //Goes through the list of left flippers and activates them
        foreach (Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.UnFlip();
        }
    }

    public void ActivateRightFlippers()
    {
        GameplayManagers.Instance.UI.RightFlipperButtonPressed();
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.Flip();
        }
    }

    public void DeactivateRightFlippers()
    {
        GameplayManagers.Instance.UI.RightFlipperButtonPassive();
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.UnFlip();
        }
    }


    public void ResetLists()
    {
        _leftFlippers.Clear();
        _rightFlippers.Clear();
    }
}
