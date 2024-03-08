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
    private float ballXVelocity;
    private float ballYVelocity;
    private Vector2 ballVelocity = new Vector2(0,0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SoundManager.Instance.PlaySFX("Laser 2");

            SplitterHit(collision.gameObject.transform.position, collision.gameObject);

        }
    }

    private void SplitterHit(Vector3 spawnLoc, GameObject oldBall)
    {
        SplitBall(spawnLoc, oldBall);
        DestroySplitter();
        SwitchToDestroyedVisuals();
    }

    void SplitBall(Vector3 spawnLoc, GameObject oldBall)
    {
        GameObject newBall = Instantiate(_ballPrefab, spawnLoc, Quaternion.identity);
        newBall.GetComponent<Rigidbody2D>().velocity = ballVelocity;
        ballXVelocity = oldBall.gameObject.GetComponent<Rigidbody2D>().velocity.x * -1;
        ballYVelocity = oldBall.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        ballVelocity = new Vector2 (ballXVelocity, ballYVelocity);
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
