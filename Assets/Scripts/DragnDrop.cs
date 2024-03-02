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
    private GameStateManager gameStateManager;
    
    private void Start()
    {
        spawningObjects = FindObjectOfType<SpawningObjects>();
        gameStateManager = FindObjectOfType<GameStateManager>();

        originalPosition = transform.position;
       
    }

    public void OnMouseDrag()
    {
        if (!dragging && gameStateManager.GPS != GameStateManager.GamePlayState.End)
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
        dragging = false;
        Vector3 newPosition = transform.position;

        if (!InvalidLocation(newPosition))
        {
            transform.position = originalPosition;
        }
        else
        {
            placeable.GetComponent<IPlaceable>().Placed();
            spawningObjects.StartSpawnDelay(gameObject);
            //spawningObjects.SpawnNewObject(gameObject);
        }
    }
    
    private bool InvalidLocation(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger && collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;   
    }

    public void SetDragging(bool enabled)
    {
        dragging = enabled;
    }
}
