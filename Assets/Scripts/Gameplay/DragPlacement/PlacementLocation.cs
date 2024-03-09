using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementLocation : MonoBehaviour
{
    [SerializeField] Collider2D _placementAreaHitbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 ClosestValidPlacementLocation(Vector2 inPos)
    {
        return Physics2D.ClosestPoint(inPos, _placementAreaHitbox);
    }
}
