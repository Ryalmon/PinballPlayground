using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DragPoint : MonoBehaviour
{
    public float TimeDragging => Time.time - timeCreated;
    private float timeCreated;
    internal bool CanPickUp = true;

    private void Start()
    {
        timeCreated = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
    }
}
