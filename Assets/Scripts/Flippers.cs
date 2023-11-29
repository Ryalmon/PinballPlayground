using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippers : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _flipUpTime;
    [SerializeField] private float _flipWaitTime;
    [SerializeField] private float _flipDownTime;
    [SerializeField] private float _upSpeed;
    [SerializeField] private float _downSpeed;
    [SerializeField] private bool _rightFlipper;
    private Coroutine _flipCoroutine;
    /*    [SerializeField] private float _upperLimit;
    private float _lowerLimit;*/
    //[SerializeField] private Quaternion _upperLimit;
    //private Quaternion _lowerLimit;
    /*    [SerializeField] HingeJoint2D _hJ;
        [SerializeField] JointMotor2D _motorJoint;*/

    private void Start()
    {
        //_lowerLimit = transform.rotation;
        //_lowerLimit = transform.rotation.eulerAngles.z;
        if(_rightFlipper)
        {
            _upSpeed *= -1;
            _downSpeed *= -1;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Flip();
        }
        /*Debug.Log(_hJ.limitState);*/
    }

    public void Flip()
    {
        if(_flipCoroutine != null)
        {
            return;
        }
        //_flipping = true;
        _flipCoroutine = StartCoroutine(FlipProcess());
    }

    IEnumerator FlipProcess()
    {
        float tempTime = 0;
        while(tempTime < _flipUpTime)
        {
            tempTime += Time.deltaTime;
            rb.angularVelocity = _upSpeed;
            //transform.rotation = Quaternion.Lerp(tempRotation, _upperLimit, tempTime);
            yield return new WaitForFixedUpdate();
        }
        rb.angularVelocity = 0;
        yield return new WaitForSeconds(_flipWaitTime);
        
        tempTime = 0;

        while(tempTime < _flipDownTime)
        {
            tempTime += Time.deltaTime;
            rb.angularVelocity = -_downSpeed;
            //transform.rotation = Quaternion.Lerp(tempRotation, _lowerLimit, tempTime);
            yield return new WaitForFixedUpdate();
        }
        
        rb.angularVelocity = 0;
        _flipCoroutine = null;
    }
}
