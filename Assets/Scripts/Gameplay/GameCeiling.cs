using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCeiling : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() == null) return;
        GameObject newGO = new GameObject();
        newGO.transform.position = collision.GetContact(0).point;
        GameplayManagers.Instance.Score.CreatePointParticles(newGO, ScoreSource.Ceiling);
        Destroy(newGO, 5);
    }
}
