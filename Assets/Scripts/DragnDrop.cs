using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragnDrop : MonoBehaviour
{
    [SerializeField] GameObject placeable;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private SpawningObjects spawningObjects;
    
    private void Start()
    {
        spawningObjects = FindObjectOfType<SpawningObjects>();
        originalPosition = transform.position;
    }

    public void OnMouseDrag()
    {
        if (!dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
    private void OnMouseDown()
    {
        if (!dragging)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void OnMouseUp()
    {
        if (!dragging)
        {
            dragging = true;
            Vector3 newPosition = transform.position;
            if (!InvalidLocation(newPosition))
            {
                transform.position = originalPosition;
                dragging = false;
                Debug.Log("Placed");
            }

            else
            {
                Debug.Log("Placed");
                placeable.GetComponent<IPlaceable>().Placed();
                spawningObjects.SpawnNewObject(gameObject);
            }
        }
    }
    
    private bool InvalidLocation(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                Debug.Log("Placed");
                placeable.GetComponent<IPlaceable>().Placed();
                return true;
            }
        }
        return false;
      
    }

    public void SetDragging(bool enabled)
    {
        Debug.Log("dragging set to: " + enabled);
        dragging = enabled;
    }
}
