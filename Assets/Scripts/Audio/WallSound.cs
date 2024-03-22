using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallPhysics>() != null)
        {
            UniversalManager.Instance.Sound.PlaySFX("8Hit");
            //SoundManager.Instance.PlaySFX("8Hit");
        }
    }
}
