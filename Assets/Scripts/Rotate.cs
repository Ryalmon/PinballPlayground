using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rotate : MonoBehaviour
{
    private Vector3 currentRotation;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _flipTime;

    [SerializeField] private Vector3 _turnEnd1;
    [SerializeField] private Vector3 _turnEnd2;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotation());
        StartCoroutine(Rotate2());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Rotation()
    {
        while (true)
        {
            //transform.position += transform.forward * PlaneSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, _turnEnd1, _rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Rotate2()
    {
        while (true)
        {
            yield return new WaitForSeconds(_flipTime);
            _rotateSpeed *= -1;
        }
    }
}
