using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCeiling : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null)
            GameplayParent.Instance.Score.CreatePointParticles(collision.gameObject, ScoreSource.Ceiling);
    }
}
