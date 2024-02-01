using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float _holdDuration;
    [SerializeField] float _resetDuration;
    [SerializeField] Vector3 _moveDistance;
    [Space]

    [Header("Refrences")]
    [SerializeField] Collider2D _detectionArea;
    [SerializeField] GameObject _holdingLocation;

    private GameObject _dragObject;
    private BallPhysics _dragObjectPhysics;
    private Vector3 startLocation;
    SpaceShipState _shipState = SpaceShipState.IDLE;
    // Start is called before the first frame update
    void Start()
    {
         startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BallPhysics>() != null)
        {
            ChangeShipState(SpaceShipState.DRAGGING);
            DragObject(collision.gameObject);
        }
    }

    private void DragObject(GameObject newDragObject)
    {
        _dragObject = newDragObject;
        _dragObjectPhysics = _dragObject.GetComponent<BallPhysics>();
        _dragObjectPhysics.ResetVelocity();
        _dragObjectPhysics.PhysicsEnabled(false);
        _dragObjectPhysics.SetPosition(_holdingLocation.transform.position);

        _dragObjectPhysics.SetParent(gameObject);

        StartCoroutine(DragProcess());
    }

    private IEnumerator DragProcess()
    {
        float moveProgress = 0;

        Vector3 moveTo = startLocation + _moveDistance;
        while(moveProgress < 1)
        {
            moveProgress += Time.deltaTime / _holdDuration;
            transform.position = Vector3.Lerp(startLocation, moveTo, moveProgress);
            yield return null;
        }
        //yield return new WaitForSeconds(_holdDuration);
        ReleaseObject();
    }

    private void ReleaseObject()
    {
        _dragObjectPhysics.PhysicsEnabled(true);
        _dragObjectPhysics.RemoveParent();

        _dragObject = null;
        _dragObjectPhysics = null;

        ChangeShipState(SpaceShipState.RESETTING);
    }

    private IEnumerator ResetSpaceShip()
    {
        yield return new WaitForSeconds(_resetDuration);
        transform.position = startLocation;
        ChangeShipState(SpaceShipState.IDLE);
    }

    private void ChangeShipState(SpaceShipState newState)
    {
        _shipState = newState;
        switch(newState)
        {
            case SpaceShipState.IDLE:
                _detectionArea.enabled = true;
                return;
            case SpaceShipState.DRAGGING:
                _detectionArea.enabled = false;
                return;
            case SpaceShipState.RESETTING:
                StartCoroutine(ResetSpaceShip());
                    
                return;
        }
            
    }
}

enum SpaceShipState
{
    IDLE,
    DRAGGING,
    RESETTING
};
