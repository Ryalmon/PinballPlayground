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
    //private Collider2D invalidCollider;

    private void Start()
    {
        spawningObjects = FindObjectOfType<SpawningObjects>();
        originalPosition = transform.position;
        //invalidCollider = GetComponent<Collider2D>();
    }

    private void OnMouseDrag()
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

    private void OnMouseUp()
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
                //placeable.GetComponent<IPlaceable>().Placed();
            }

            else
            {
                Debug.Log("Placed");
                placeable.GetComponent<IPlaceable>().Placed();
                spawningObjects.SpawnNewObject(gameObject);
            }
            //spawningObjects.SpawnNewObject(gameObject);
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
}
