using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    [SerializeField] Vector3 MoveLocation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            transform.position += MoveLocation * Time.deltaTime;
            yield return null;
        }

    }
    void Update()
    {
        
    }

    public void SetMoveDirection(Vector3 direction)
    {
        MoveLocation = direction;
    }
}
