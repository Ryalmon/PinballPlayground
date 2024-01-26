using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipperManager : MonoBehaviour
{
    public static FlipperManager M_Instance;
    [SerializeField] List<Flippers> _leftFlippers = new List<Flippers>();
    [SerializeField] List<Flippers> _rightFlippers = new List<Flippers>();

    private void Start()
    {
    }

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
        }
    }

    public void ActivateRightFlippers()
    {
        //Goes through the list of right flippers and activates them
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.Flip();
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
