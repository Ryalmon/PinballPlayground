using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragnDrop : MonoBehaviour
{
    [SerializeField] float _minDistanceForValidPlacement;
    [Space]

    [SerializeField] float _travelToSpawnTime;
    [SerializeField] float _minDistForPlacement;
    [SerializeField] float _minSpeedDist;
    [SerializeField] float _maxSpeedDist;

    private DragTokenSO _placementData;

    private bool dragging = false;

    private Vector3 offset;
    private Vector3 originalPosition;
    
    private void Start()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectIn(gameObject, GameplayManagers.Instance.Placement.GetTokenFadeInTime(), null);
        originalPosition = transform.position;
    }

    public void AssignPlacementData(DragTokenSO newPlacementData)
    {
        _placementData = newPlacementData; 
        UpdatePlacementVisuals();
    }

    private void UpdatePlacementVisuals()
    {
        GetComponent<SpriteRenderer>().sprite = _placementData._tokenVisuals;
    }

    public void OnMouseDrag()
    {
        if (!dragging && GameplayManagers.Instance.State.GPS != GameStateManager.GamePlayState.End)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
    private void OnMouseDown()
    {
        if (!dragging)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameplayManagers.Instance.Placement.IncreaseItemsBeingDragged();
        }
    }

    public void OnMouseUp()
    {
        dragging = false;

        AttemptPlacement();
    }
    
    private void AttemptPlacement()
    {
        GameplayManagers.Instance.Placement.DecreaseItemsBeingDragged();

        if (!CheckLocationValidity(transform.position))
        {
            transform.position = originalPosition;
        }
        else
        {
            PlaceItem();
        }
    }

    private bool CheckLocationValidity(Vector2 positionToCheck)
    {
        if (Vector2.Distance(positionToCheck, originalPosition) > _minDistanceForValidPlacement)
            return true;
        return false;
        /*Collider2D[] colliders = Physics2D.OverlapPointAll(position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger && collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;*/   
    }

    /*
    private void ValidPlacement()
    {
        StartCoroutine(MoveTokenToNewPos(new Vector3(transform.position.x,-4, transform.position.z)));
    }*/

    private void PlaceItem()
    {
        Vector2 validPlacementLocation = GameplayManagers.Instance.Placement.ClosestValidPlacementLocation(transform.position);
        StartCoroutine(MoveTokenToNewPos(validPlacementLocation));
    }

    private IEnumerator MoveTokenToNewPos(Vector3 targetPos)
    {
        float progress = 0;
        Vector3 startPos = transform.position;
        float currentDist = Vector2.Distance(transform.position, targetPos);

        while (currentDist > _minDistForPlacement)
        {
            currentDist = Vector2.Distance(transform.position, targetPos);
            float speedFromDistance = Mathf.Clamp(currentDist, _minSpeedDist, _maxSpeedDist);

            progress += Time.deltaTime / _travelToSpawnTime * speedFromDistance;
            transform.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;
                
        }

        CreateTokenPlaceable();
    }

    private void CreateTokenPlaceable()
    {
        GameObject spawnedPlaceable = Instantiate(_placementData._objectToSpawn, transform.position, Quaternion.identity);
        spawnedPlaceable.GetComponentInChildren<SpriteRenderer>().sortingOrder = 
            GameplayManagers.Instance.Spawning.GetCurrentObjectLayer();

        IPlaceable _placeableInterface;
        if (spawnedPlaceable.GetComponent<IPlaceable>() != null)
            _placeableInterface = spawnedPlaceable.GetComponent<IPlaceable>();
        else if (spawnedPlaceable.GetComponentInChildren<IPlaceable>() != null)
            _placeableInterface = spawnedPlaceable.GetComponentInChildren<IPlaceable>();
        else return;

        _placeableInterface.Placed();

        //spawningObjects.StartSpawnDelay(gameObject);
        GameplayManagers.Instance.Spawning.PlaceableObjectPlaced(gameObject);
        
        Destroy(gameObject);
    }

    public void SetDragging(bool enabled)
    {
        dragging = enabled;
    }
}
