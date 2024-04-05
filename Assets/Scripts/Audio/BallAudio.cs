using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    private void OnCollision2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            UniversalManager.Instance.Sound.PlaySFX("HitWall");
            //SoundManager.Instance.PlaySFX("Hit");
        }

        if (collision.gameObject.CompareTag("Bumper"))
        {
            UniversalManager.Instance.Sound.PlaySFX("HitBumper");
            //SoundManager.Instance.PlaySFX("Bounce");
            Debug.Log("Bumped");
        }
    }
    
}
