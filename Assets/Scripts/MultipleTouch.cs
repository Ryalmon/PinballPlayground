using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MultipleTouch : MonoBehaviour
{
    // Vars
    [SerializeField] private GameObject circle;
    private Camera mainCam;
    private List<TouchLocations> touches = new List<TouchLocations>();

    private Coroutine _touchProcess;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        CameraUpdate();
        UniversalManager.Instance.Scene.PostSceneChangeEvent().AddListener(CameraUpdate);

        StartTouchProcess();

        // I know GameObject.Find is the worst thing ever but I am tired and struggling to find a better way.
        // Given that is manager is a PREFAB (??????) and doesnt even have a constructor.
        // Also the MultipleTouch class is never even referenced so its basically impossible for me to find the code
        // that instantiates this manager, so i can set the damn variable from that script.
        // 
        // In the future, if you are going to make a manager script that is instantiated from another script, DO NOT
        // make the script inherit monobehaviour.
        //gameObjectsCanvas = GameObject.Find("GameObjects").GetComponent<RectTransform>(); 
        //                  ok so the code ended up not working but let this be a lesson in good system architecture
    }

    public void StartTouchProcess()
    {
        if(_touchProcess == null)
        {
            _touchProcess = StartCoroutine(TouchProcess());
        }
    }

    public void StopTouchProcess()
    {
        if(_touchProcess != null)
        {
            StopCoroutine(_touchProcess);
            _touchProcess = null;
        }
    }

    private IEnumerator TouchProcess()
    {
        while(true)
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
                    if(thisTouch !=null)
                    {
                        Destroy(thisTouch.circle);
                        touches.RemoveAt(touches.IndexOf(thisTouch));
                    }
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

            yield return null;
        }
    }

    /// <summary>
    /// Changes touch coords from screen space to world space
    /// </summary>
    /// <param name="touchPosition">screen coords of touch</param>
    /// <returns>world coords of touch</returns>
    Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        if (mainCam == null)
            CameraUpdate();

        Vector3 rawPoint = mainCam.GetComponent<Camera>().ScreenToWorldPoint(
            new Vector3(touchPosition.x, touchPosition.y, 0));

        return rawPoint;
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
        Vector3 position = GetTouchPosition(t.position);
        c.transform.position = position;

        // Debug draw tocuh position
        Debug.DrawLine(position + (Vector3.down / 2), position + (Vector3.up / 2), UnityEngine.Color.green);
        Debug.DrawLine(position + (Vector3.left / 2), position + (Vector3.right / 2), UnityEngine.Color.green);

        return c;
    }

    void CameraUpdate()
    {
        mainCam = Camera.main;
    }
}