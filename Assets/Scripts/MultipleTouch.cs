using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public List<TouchLocation> touches = new List<TouchLocation>();

    void Update()
    {
        int i = 0;
        while(i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began)
            {
                touches.Add(new TouchLocation(t.fingerId));
            }
            else if(t.phase == TouchPhase.Ended)
            {
                TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchID == t.fingerId);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            else if(t.phase == TouchPhase.Moved)
            {
                TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchID == t.fingerId);
            }
        }
    }
    Vector3 getTouchPosition(Vector3 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, touchPosition.z));
    }

}
