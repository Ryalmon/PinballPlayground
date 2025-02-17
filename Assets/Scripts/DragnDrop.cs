using System.Collections;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
    [SerializeField] float _minDistanceForValidPlacement;
    [Space]

    [SerializeField] float _travelToSpawnTime;
    [SerializeField] float _minDistForPlacement;
    [SerializeField] float _minSpeedDist;
    [SerializeField] float _maxSpeedDist;

    private DragTokenSO _placementData;

    bool isFollowingTouch = false;

    private Vector3 originalPosition;

    bool onlyCollideOnce = false;
    bool stoppedFollowing = false;
    bool failsafeTriggered = false;
    [SerializeField] Collider2D circleTrigger;
    Transform playerTouch;

    DragPoint _currentDragPoint;

    private void Start()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectIn(gameObject, GameplayManagers.Instance.Placement.GetTokenFadeInTime(), null);
        originalPosition = transform.position;
    }

    private void Update()
    {
        // Failsafe
        if (!failsafeTriggered && GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            Debug.Log("Failsafe triggered");
            failsafeTriggered = true;
        }
        else if (isFollowingTouch && playerTouch != null)
        {

            transform.position = (Vector2)playerTouch.position;
            
        }
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
    
    private void AttemptPlacement()
    {
        GameplayManagers.Instance.Placement.DecreaseItemsBeingDragged();

        if (!CheckLocationValidity(transform.position))
        {
            transform.position = originalPosition;
        }
        else
        {
            onlyCollideOnce = true;
            if (circleTrigger != null)
            {
                Destroy(circleTrigger);
            }
            PlaceItem();
        }
    }

    private bool CheckLocationValidity(Vector2 positionToCheck)
    {
        if (Vector2.Distance(positionToCheck, originalPosition) > _minDistanceForValidPlacement)
            return true;
        return false;
    }

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
        GameplayManagers.Instance.Spawning.ReshuffleSpecificToken(_placementData);

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

        GameplayManagers.Instance.Spawning.PlaceableObjectPlaced(gameObject);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentDragPoint = collision.GetComponent<DragPoint>();
        if (!onlyCollideOnce && _currentDragPoint != null && _currentDragPoint.CanPickUp)
        {
            GameplayManagers.Instance.Placement.IncreaseItemsBeingDragged();
            isFollowingTouch = true;
            playerTouch = collision.gameObject.transform;
            _currentDragPoint.CanPickUp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTouch"))
        {
            StopFollowing();
            AttemptPlacement();
        }
    }

    private void StopFollowing()
    {
        if (!stoppedFollowing)
        {
            stoppedFollowing = true;

            isFollowingTouch = false;
            playerTouch = null;
        }
    }
}
