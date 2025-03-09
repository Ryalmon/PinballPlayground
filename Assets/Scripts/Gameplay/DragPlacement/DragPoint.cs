using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DragPoint : MonoBehaviour
{
    internal bool CanPickUp = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
    }
}
