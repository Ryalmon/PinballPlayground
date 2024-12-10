using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCeiling : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlaceableCheck(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlaceableCheck(collision.gameObject);        
    }

    void PlaceableCheck(GameObject newObj)
    {
        IPlaceable placeable = newObj.GetComponent<IPlaceable>();
        Drift driftComponent = newObj.GetComponent<Drift>();
        if (driftComponent == null)
            driftComponent = newObj.GetComponentInParent<Drift>();

        if (placeable != null && driftComponent != null)
        {
            placeable.DestroyPlacedObject();
        }
    }
}
