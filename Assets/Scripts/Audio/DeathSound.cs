using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null)
        {
            UniversalManager.Instance.Sound.PlaySFX("Death");
            //SoundManager.Instance.PlaySFX("Death");
        }
    }
}
