using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippers : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _flipUpTime;
    [SerializeField] private float _flipWaitTime;
    [SerializeField] private float _flipDownTime;
    [SerializeField] private Quaternion _upperLimit;
    private Quaternion _lowerLimit;
    /*    [SerializeField] HingeJoint2D _hJ;
        [SerializeField] JointMotor2D _motorJoint;*/

    private void Start()
    {
        _lowerLimit = transform.rotation;
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
        StartCoroutine(FlipProcess());
    }

    IEnumerator FlipProcess()
    {
        Quaternion tempRotation = transform.rotation;
        float tempTime = 0;
        while(tempTime < 1)
        {
            tempTime += Time.deltaTime / _flipUpTime;
            rb.angularVelocity = 100;
            //transform.rotation = Quaternion.Lerp(tempRotation, _upperLimit, tempTime);
            yield return null;
        }
        Debug.Log("Done");
        yield return new WaitForSeconds(_flipWaitTime);
        tempRotation = transform.rotation;
        tempTime = 0;

        while(tempTime < 1)
        {
            tempTime += Time.deltaTime / _flipDownTime;
            rb.angularVelocity = -30;
            //transform.rotation = Quaternion.Lerp(tempRotation, _lowerLimit, tempTime);
            yield return null;
        }

        rb.angularVelocity = 0;

    }

    /*IEnumerator FlipProcess()
    {
        
        MotorSpeed(_flipUp);

        while (_hJ.limitState != JointLimitState2D.LowerLimit)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(_flipWait);

        Debug.Log("30");
        MotorSpeed(_flipDown);
        while (_hJ.limitState != JointLimitState2D.UpperLimit)
        {
            Debug.Log("A");
            yield return null;
        }
        
        MotorSpeed(0);
    }

    void MotorSpeed(float speed)
    {
        _motorJoint = _hJ.motor;
        _motorJoint.motorSpeed = speed;
        _hJ.motor = _motorJoint;
    }*/
}
