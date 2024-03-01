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
        //Goes through the list of left flippers and activates them
        foreach(Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.Flip();
            SoundManager.Instance.PlaySFX("Hit");
        }
    }

    public void DeactivateLeftFlippers()
    {
        //Goes through the list of left flippers and activates them
        foreach (Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.UnFlip();
        }
    }

    public void ActivateRightFlippers()
    {
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.Flip();
            SoundManager.Instance.PlaySFX("Hit");
        }
    }

    public void DeactivateRightFlippers()
    {
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.UnFlip();
        }
    }

    /*    public void AttachButtons()
        {
            GameObject LeftButton = GameObject.Find("LeftFlipperButton");
            LeftButton.GetComponent<Button>().onClick.AddListener(ActivateLeftFlippers);
            if (LeftButton.GetComponent<Button>() != null)
                Debug.Log("FoundLeftButton");
        }*/

    public void ResetLists()
    {
        _leftFlippers.Clear();
        _rightFlippers.Clear();
    }
}
