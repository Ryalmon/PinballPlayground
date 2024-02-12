using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float _holdDuration;
    [SerializeField] float _resetDuration;
    [SerializeField] Vector3 _moveDistance;
    [SerializeField] float _releaseForce;
    [SerializeField] float _releaseXVariability;
    [SerializeField] int _baseScoreValue;
    [Space]

    [Header("Refrences")]
    [SerializeField] Collider2D _detectionArea;
    [SerializeField] GameObject _holdingLocation;

    private GameObject _dragObject;
    private BallPhysics _dragObjectPhysics;
    SpaceShipState _shipState = SpaceShipState.IDLE;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BallPhysics>() != null && _shipState == SpaceShipState.IDLE )
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

        Vector3 moveTo = _moveDistance;
        while(moveProgress < 1)
        {
            moveProgress += Time.deltaTime / _holdDuration;
            transform.localPosition = Vector3.Lerp(Vector3.zero, moveTo, moveProgress);
            yield return null;
        }
        //yield return new WaitForSeconds(_holdDuration);
        ReleaseObject();
    }

    private void ReleaseObject()
    {
        _dragObjectPhysics.PhysicsEnabled(true);
        _dragObjectPhysics.RemoveParent();

        Vector2 releaseBallForce = new Vector2(Random.Range(-_releaseXVariability, _releaseXVariability), _releaseForce);
        _dragObjectPhysics.OverrideBallForce(releaseBallForce);

        _dragObject = null;
        _dragObjectPhysics = null;

        GameplayParent.Instance.Score.CreatePointParticles(gameObject, _baseScoreValue);

        ChangeShipState(SpaceShipState.RESETTING);
    }

    private IEnumerator ResetSpaceShip()
    {
        yield return new WaitForSeconds(_resetDuration);
        transform.localPosition = Vector3.zero;
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
