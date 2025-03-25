using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/*
 * Made by toby in a blind fit of rage
 */
public class FlipperButton : MonoBehaviour
{
    [SerializeField] private FlipperManager flipperManager;

    public UnityEvent OnPointerDown;
    public UnityEvent OnPointerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DragPoint>(out DragPoint dp))
        {
            Debug.Log(dp.TimeDragging);
            if(dp.TimeDragging < 0.25f)
                OnPointerDown.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DragPoint>(out DragPoint dp))
        {
            OnPointerUp.Invoke();
        }
    }
}
