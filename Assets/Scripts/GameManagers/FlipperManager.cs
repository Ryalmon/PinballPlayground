using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperManager : MonoBehaviour
{
    public static FlipperManager M_Instance;
    [SerializeField] List<Flippers> _leftFlippers = new List<Flippers>();
    [SerializeField] List<Flippers> _rightFlippers = new List<Flippers>();

    public void AddToList(Flippers newFlipper, List<Flippers> flipList)
    {
        flipList.Add(newFlipper);
    }

    void ActivateLeftFlippers()
    {
        foreach(Flippers currentFlipper in _leftFlippers)
        {
            currentFlipper.Flip();
        }
    }

    void ActivateRightFlippers()
    {
        foreach (Flippers currentFlipper in _rightFlippers)
        {
            currentFlipper.Flip();
        }
    }

    public void ResetLists()
    {
        _leftFlippers.Clear();
        _rightFlippers.Clear();
    }
}
