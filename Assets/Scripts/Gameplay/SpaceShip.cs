using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour, IPlaceable
{
    [Header("Variables")]
    [SerializeField] float _holdDuration;
    [SerializeField] float _resetDuration;
    [SerializeField] Vector3 _moveDistance;
    [SerializeField] float _releaseForce;
    [SerializeField] float _releaseXVariability;
    [Space]

    [Header("Refrences")]
    [SerializeField] Collider2D _detectionArea;
    [SerializeField] GameObject _holdingLocation;

    private GameObject _dragObject;
    private BallPhysics _dragObjectPhysics;
    private Coroutine _idleMovementCoroutine;
    private Coroutine _dragMovementCoroutine;
    public float _idleXVariability = 1;
    public float _idleYVariability = 1;
    SpaceShipState _shipState = SpaceShipState.IDLE;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null && _shipState == SpaceShipState.IDLE)
        {
            ChangeShipState(SpaceShipState.DRAGGING);
            DragObject(collision.gameObject);
        }
    }

    private void StartIdleMovement()
    {
        _idleMovementCoroutine = StartCoroutine(IdleMovement());
    }

    private IEnumerator IdleMovement()
    {
        float time = 0;
        while (_shipState == SpaceShipState.IDLE)
        {
            time += Time.deltaTime;
            float x = Mathf.Cos(time);
            float y = Mathf.Sin(2 * time) / 2;
            transform.localPosition = new Vector2((x * _idleXVariability), (y * _idleYVariability));
            yield return null;
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

        _dragMovementCoroutine = StartCoroutine(DragProcess());
    }

    private IEnumerator DragProcess()
    {
        float moveProgress = 0;
        Vector3 startPos = transform.localPosition;
        Vector3 moveTo = _moveDistance + startPos;
        //Vector3 testMoveTo = moveTo + startPos;
        //Debug.Log("StartPOS: " + startPos + " MoveTo " + moveTo + " testMoveTo " + testMoveTo);
        while(moveProgress < 1)
        {
            moveProgress += Time.deltaTime / _holdDuration;
            transform.localPosition = Vector3.Lerp(startPos, moveTo, moveProgress);
            yield return null;
        }
        //yield return new WaitForSeconds(_holdDuration);
        _dragMovementCoroutine = null;
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

        GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.SpaceShip);

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
                StartIdleMovement();
                return;
            case SpaceShipState.DRAGGING:
                _detectionArea.enabled = false;
                return;
            case SpaceShipState.RESETTING:
                StartCoroutine(ResetSpaceShip());
                    
                return;
        }
            
    }

    public void Placed()
    {
        GetComponentInParent<Drift>().enabled = true;
        StartIdleMovement();
    }

    public void DestroyPlacedObject()
    {
        if (_dragMovementCoroutine != null)
        {
            StopCoroutine(_dragMovementCoroutine);
            ReleaseObject();
            return;
        }
        Destroy(transform.parent.gameObject);
    }
}

enum SpaceShipState
{
    IDLE,
    DRAGGING,
    RESETTING
};
