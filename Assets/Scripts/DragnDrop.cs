using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragnDrop : MonoBehaviour
{
    [SerializeField] DragTokenSO _placementData;
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

    public void AssignPlacementData(DragTokenSO newPlacementData)
    {
        _placementData = newPlacementData;
        placeable = _placementData._objectToSpawn;
        UpdatePlacementVisuals();
    }

    private void UpdatePlacementVisuals()
    {
        GetComponent<SpriteRenderer>().sprite = _placementData._tokenVisuals;
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

        AttemptPlacement();
    }
    
    private void AttemptPlacement()
    {
        if (!CheckLocationValidity(transform.position))
        {
            transform.position = originalPosition;
        }
        else
        {
            ValidPlacement();
        }
    }

    private bool CheckLocationValidity(Vector3 position)
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

    private void ValidPlacement()
    {
        GameObject spawnedPlaceable = Instantiate(_placementData._objectToSpawn, transform.position, Quaternion.identity);

        IPlaceable _placeableInterface;
        if (spawnedPlaceable.GetComponent<IPlaceable>() != null)
            _placeableInterface = spawnedPlaceable.GetComponent<IPlaceable>();
        else if (spawnedPlaceable.GetComponentInChildren<IPlaceable>() != null)
            _placeableInterface = spawnedPlaceable.GetComponentInChildren<IPlaceable>();
        else return;

        _placeableInterface.Placed();

        //placeable.GetComponent<IPlaceable>().Placed();
        spawningObjects.StartSpawnDelay(gameObject);
        //spawningObjects.SpawnNewObject(gameObject);
        Destroy(gameObject);
    }

    public void SetDragging(bool enabled)
    {
        dragging = enabled;
    }
}
