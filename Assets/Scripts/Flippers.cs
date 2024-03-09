using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flippers : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _flipUpTime;
    [SerializeField] private float _flipDownTime;
    [SerializeField] private float _upSpeed;
    [SerializeField] private float _downSpeed;
    [SerializeField] private bool _rightFlipper;

    [SerializeField] private bool hold;
    private Coroutine _flipCoroutine;
    private Coroutine _unflipCoroutine;

    [SerializeField] private Quaternion _startingRotation;

    private void Start()
    {
        //_lowerLimit = transform.rotation;
        //_lowerLimit = transform.rotation.eulerAngles.z;
        _startingRotation = transform.rotation;
        if(_rightFlipper)
        {
            _upSpeed *= -1;
            _downSpeed *= -1;
        }
    }

    public void Flip()
    {
        //Checks that you are not currently flipping
        if(_flipCoroutine != null)
        {
            return;
        }
        //Assignes the coroutine value and starts the process of flipping
        _flipCoroutine = StartCoroutine(FlipProcess());
    }

    public void UnFlip()
    {
        hold = false;
    }

    IEnumerator FlipProcess()
    {
        hold = true;
        UniversalManager.Instance.Sound.PlaySFX("FlipUp");
        float tempTime = 0;
        while (tempTime < _flipUpTime)
        {
            //Flips the flipper up until a set time has passed
            tempTime += Time.deltaTime;
            rb.angularVelocity = _upSpeed;
            //transform.rotation = Quaternion.Lerp(tempRotation, _upperLimit, tempTime);
            yield return new WaitForFixedUpdate();
        }
        //Stops the flipping to wait
        rb.angularVelocity = 0;
        while (hold)
            yield return null;
        StartCoroutine(UnFlipProcess());
        /*if (hold == false && done == true)
        {
            StartCoroutine(UnFlipProcess());
        }*/
    }
    IEnumerator UnFlipProcess()
    {
        float tempTime = 0;

        UniversalManager.Instance.Sound.PlaySFX("FlipDown");
        while (tempTime < _flipDownTime)
        {
            //Flips the flippers down until a set time has passed
            tempTime += Time.deltaTime;
            rb.angularVelocity = -_downSpeed;
            //transform.rotation = Quaternion.Lerp(tempRotation, _lowerLimit, tempTime);
            yield return new WaitForFixedUpdate();
        }
        
        rb.angularVelocity = 0;
        _flipCoroutine = null;
        transform.rotation = _startingRotation;
    }
}
