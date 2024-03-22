using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocation : MonoBehaviour
{
    public int touchID;

    public TouchLocation(int newTouchId)
    {
        touchID = newTouchId;
    }
}
