using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSplit : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float _destroyTime;

    [Header("References")]
    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Sprite _destroyedVisuals;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SoundManager.Instance.PlaySFX("Laser 2");

            SplitterHit(collision.gameObject.transform.position);
        }
    }

    private void SplitterHit(Vector3 spawnLoc)
    {
        SplitBall(spawnLoc);
        DestroySplitter();
        SwitchToDestroyedVisuals();
    }

    void SplitBall(Vector3 spawnLoc)
    {
        GameObject newBall = Instantiate(_ballPrefab, spawnLoc, Quaternion.identity);
        GameplayManagers.Instance.Ball.AddBall(newBall);
    }

    void DestroySplitter()
    {
        GetComponent<Collider2D>().enabled = false;

        GameplayManagers.Instance.Fade.FadeGameObjectOut(gameObject, _destroyTime, null);
        Destroy(gameObject, _destroyTime);
    }

    void SwitchToDestroyedVisuals()
    {
        GetComponent<SpriteRenderer>().sprite = _destroyedVisuals;
    }
}
