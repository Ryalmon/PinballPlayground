using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flippers : MonoBehaviour
{
    [SerializeField] float _flipUp;
    [SerializeField] float _flipWait;
    [SerializeField] float _flipDown;
    [SerializeField] HingeJoint2D _hJ;
    [SerializeField] JointMotor2D _motorJoint;
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
        //StartCoroutine(FlipProcess());
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
