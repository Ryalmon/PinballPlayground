using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCeiling : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null)
        {
            GameObject newGO = new GameObject();
            newGO.transform.position = collision.GetContact(0).point;
            GameplayManagers.Instance.Score.CreatePointParticles(newGO, ScoreSource.Ceiling);
            Destroy(newGO, 5);
            return;
        }

        PlaceableCheck(collision.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlaceableCheck(collision.gameObject);        
    }

    void PlaceableCheck(GameObject newObj)
    {
        IPlaceable placeable = newObj.GetComponent<IPlaceable>();
        if (placeable != null)
        {
            placeable.DestroyPlacedObject();
        }
    }
}
