using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShip : MonoBehaviour, IPlaceable
{
    [Header("Variables")]
    [SerializeField] float _holdDuration;
    [SerializeField] float _resetDuration;
    [SerializeField] Vector3 _moveDistance;
    [SerializeField] float _releaseXVariability;
    [SerializeField] float _releaseYForce;
    [SerializeField] float _minSpeedToAddXVariability;
    [SerializeField] float _resetFadeOutTime;
    [SerializeField] float _resetFadeInTime;
    [Space]
    [SerializeField] float _destroyTime;
    //private float _storedXVelocity;
    private Vector2 _storedVelocity;
    private bool fadingOut = false;
    [Space]

    [Header("Refrences")]
    [SerializeField] Collider2D _detectionArea;
    [SerializeField] Collider2D _dragArea;
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
        if (collision.gameObject.GetComponent<BallPhysics>() != null && _shipState == SpaceShipState.IDLE && !fadingOut)
        {
            UniversalManager.Instance.Sound.PlaySFX("Hit");
            //SoundManager.Instance.PlaySFX("Hit");
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
        float time = 4.75f;
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
        StopCoroutine(_idleMovementCoroutine);

        _storedVelocity = newDragObject.GetComponent<Rigidbody2D>().velocity;

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
            //float tempMoveProgress = Mathf.Pow(moveProgress, 2) + 2 * moveProgress + 0;
            transform.localPosition = Vector3.Lerp(startPos, moveTo, moveProgress);
            yield return null;
        }
        //yield return new WaitForSeconds(_holdDuration);
        _dragMovementCoroutine = null;
        ReleaseObject();
    }

    private void ReleaseObject()
    {
        if (_dragObject == null) return;

        _dragObjectPhysics.PhysicsEnabled(true);
        _dragObjectPhysics.RemoveParent();

        if (Mathf.Abs(_storedVelocity.x) < _minSpeedToAddXVariability)
            _storedVelocity += new Vector2 (Random.Range(-_releaseXVariability, _releaseXVariability) , 0 );
        if (_storedVelocity.y < _releaseYForce)
            _storedVelocity = new Vector2(_storedVelocity.x, _releaseYForce);
        Vector2 releaseBallForce = new Vector2(_storedVelocity.x, _storedVelocity.y);
        _dragObjectPhysics.OverrideBallForce(releaseBallForce);

        _dragObject = null;
        _dragObjectPhysics = null;

        GameplayManagers.Instance.Score.CreatePointParticles(gameObject, ScoreSource.SpaceShip);

        ChangeShipState(SpaceShipState.RESETTING);
    }

    private IEnumerator ResetSpaceShip()
    {
        GameplayManagers.Instance.Fade.FadeGameObjectOut(gameObject, _resetFadeOutTime,null);
        yield return new WaitForSeconds(_resetDuration);
        transform.localPosition = Vector3.zero;

        ChangeShipState(SpaceShipState.IDLE);
        UnityEvent postFadeIn = new UnityEvent();
        postFadeIn.AddListener(DetectionAreaEnabled);
        GameplayManagers.Instance.Fade.FadeGameObjectIn(gameObject, _resetFadeInTime,postFadeIn);
    }

    private void DetectionAreaEnabled()
    {
        _detectionArea.enabled = true;
    }

    private void ChangeShipState(SpaceShipState newState)
    {
        Animator animator = GetComponent<Animator>();

        _shipState = newState;
        switch(newState)
        {
            case SpaceShipState.IDLE:
                StartIdleMovement();
                
                animator.SetTrigger("Idle");
                return;
            case SpaceShipState.DRAGGING:
                _detectionArea.enabled = false;
                animator.SetTrigger("Dragging");
                return;
            case SpaceShipState.RESETTING:
                StartCoroutine(ResetSpaceShip());
                animator.SetTrigger("Resetting");
                return;
        }
            
    }

    public void Placed()
    {
        _detectionArea.enabled = true;
        _dragArea.enabled = false;
        GetComponentInParent<Drift>().enabled = true;
        StartIdleMovement();
        GetComponent<SpaceShip>().enabled = true;
    }

    public void DestroyPlacedObject()
    {
        if (_dragMovementCoroutine != null)
        {
            StopCoroutine(_dragMovementCoroutine);
            ReleaseObject();
            return;
        }
        fadingOut = true;
        GameplayManagers.Instance.Fade.FadeGameObjectOut(gameObject, _destroyTime,null);
        Destroy(transform.parent.gameObject,_destroyTime);
    }

}

enum SpaceShipState
{
    IDLE,
    DRAGGING,
    RESETTING
};


//toby was here