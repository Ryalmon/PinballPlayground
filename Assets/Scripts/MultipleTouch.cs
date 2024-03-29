/******************************************************************************
// File Name :         MultipleTouch.cs
// Author :            Peter Campbell
// Creation Date :     December 21st 2022
//
// Brief Description : Used to display touch inputs for testing. Based off code
//                     from https://www.youtube.com/watch?v=98dQBWUyy9M
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    // Vars
    public GameObject circle;
    public Camera mainCam;
    public List<TouchLocations> touches = new List<TouchLocations>();

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Gets touches
        // Bruh the tutorial shouldve used a foreach smh
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);

            // Determines state of each touch,as well as what to do during each
            // phase
            // Beginning
            if (t.phase == TouchPhase.Began)
            {
                //Debug.Log("touch began");
                // Creates marker and assigns input to player
                touches.Add(new TouchLocations(t.fingerId, CreateCircle(t)));
                TouchLocations thisTouch = touches.Find(TouchLocations =>
                    TouchLocations.touchID == t.fingerId);

            }
            // Middle
            else if (t.phase == TouchPhase.Ended)
            {
                //Debug.Log("touch ended");
                // Removes Marker
                TouchLocations thisTouch = touches.Find(TouchLocations =>
                    TouchLocations.touchID == t.fingerId);
                Destroy(thisTouch.circle);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            // End
            else if (t.phase == TouchPhase.Moved)
            {
                //Debug.Log("touch moving");
                // Moves markers
                TouchLocations thisTouch = touches.Find(TouchLocations =>
                    TouchLocations.touchID == t.fingerId);
                thisTouch.circle.transform.position = GetTouchPosition(t.position);
            }

            // Increments to next touch and prevents loop
            i++;
        }
    }

    /// <summary>
    /// Changes touch coords from screen space to world space
    /// </summary>
    /// <param name="touchPosition">screen coords of touch</param>
    /// <returns>world coords of touch</returns>
    Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        return mainCam.GetComponent<Camera>().ScreenToWorldPoint(
            new Vector3(touchPosition.x, touchPosition.y, 0));
    }

    /// <summary>
    /// Creates a circle gameobject at the location of a touch
    /// </summary>
    /// <param name="t">Location of touch Input</param>
    /// <returns>Circle GameObject</returns>
    GameObject CreateCircle(Touch t)
    {
        //Creates the circle and assigns it to a variable
        GameObject c = Instantiate(circle);
        c.name = "touch" + t.fingerId;
        //Assigns its position to be where the touch occured
        c.transform.position = GetTouchPosition(t.position);
        return c;
    }

    /// <summary>
    /// Returns which player gave which input based on input location
    /// </summary>
    /// <param name="input">touch input</param>
    /// <returns>true == player 1, false == player 2</returns>

    /*
    public bool Player1Touch(Touch input)
    {
        if (GetTouchPosition(input.position).x <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }*/
}