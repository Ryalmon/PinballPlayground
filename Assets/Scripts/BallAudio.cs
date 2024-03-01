using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.Instance.PlaySFX("Hit");
        }

        if (collision.gameObject.CompareTag("Bumper"))
        {
            SoundManager.Instance.PlaySFX("Bounce");
            Debug.Log("Bumped");
        }
    }
    
}
