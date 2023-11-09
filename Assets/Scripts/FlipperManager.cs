using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperManager : MonoBehaviour
{
    public static FlipperManager M_Instance;
    [SerializeField] List<Flippers> _leftFlippers = new List<Flippers>();
    [SerializeField] List<Flippers> _rightFlippers = new List<Flippers>();

    /*private void Awake()
    {
        EstablishSingleton();
    }

    private void EstablishSingleton()
    {
        if (M_Instance != null && M_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        M_Instance = this;
        DontDestroyOnLoad(gameObject);
    }*/

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
