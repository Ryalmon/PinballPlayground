/**
// File Name :         TouchLocations.cs
// Author :            Peter Campbell
// Creation Date :     December 21st 2022
//
// Brief Description : Used to track inputs for testing. Based off code from
//                     https://www.youtube.com/watch?v=98dQBWUyy9M
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocations
{
    // Vars
    public int touchID;
    public GameObject circle;

    // Default Constructor
    public TouchLocations(int newTouchID, GameObject newCircle)
    {
        touchID = newTouchID;
        circle = newCircle;
    }
}