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
        if (collision.gameObject.CompareTag("PlayerTouch"))
        {
            OnPointerDown.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTouch"))
        {
            OnPointerUp.Invoke();
        }
    }
}
