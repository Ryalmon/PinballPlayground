using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float _gravityForce;
    private List<BallPhysics> _objectsInRadius = new List<BallPhysics>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObjectsInRadius());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveObjectsInRadius()
    {
        while(true)
        {
            foreach (BallPhysics bp in _objectsInRadius)
            {
                bp.ApplyForceToBall(CalculateGravityForce(bp));
            }
            yield return null;
        }
        
    }

    Vector2 CalculateGravityForce(BallPhysics bp)
    {
        Vector2 newForce = (transform.position - bp.gameObject.transform.position ) * _gravityForce * Time.deltaTime;
        return newForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallPhysics bp = collision.gameObject.GetComponent<BallPhysics>();
        if (bp != null)
        {
            _objectsInRadius.Add(bp);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BallPhysics bp = collision.gameObject.GetComponent<BallPhysics>();
        if (bp != null)
        {
            _objectsInRadius.Remove(bp);
        }
    }
}
