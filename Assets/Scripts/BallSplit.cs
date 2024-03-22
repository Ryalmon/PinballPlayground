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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (GameplayManagers.Instance.State.GPS != GameStateManager.GamePlayState.Play)
                return;
            UniversalManager.Instance.Sound.PlaySFX("Laser 2");
            //SoundManager.Instance.PlaySFX("Laser 2");

            SplitterHit(collision.gameObject);

        }
    }

    private void SplitterHit( GameObject oldBall)
    {
        DestroySplitter();
        SplitBall(oldBall);
    }

    void SplitBall(GameObject oldBall)
    {
        GameObject newBall = GameplayManagers.Instance.Ball.CreateBall(oldBall.transform.position);
        
        SetNewBallVelocity(newBall,oldBall);

        GameplayManagers.Instance.Ball.AddBall(newBall);
    }

    void SetNewBallVelocity(GameObject newBall, GameObject oldBall)
    {
        ballXVelocity = oldBall.gameObject.GetComponent<Rigidbody2D>().velocity.x * -1;
        ballYVelocity = oldBall.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        Vector2 ballVelocity = new Vector2(ballXVelocity, ballYVelocity);
        newBall.GetComponent<Rigidbody2D>().velocity = ballVelocity;
    }

    void DestroySplitter()
    {
        SwitchToDestroyedVisuals();
        GetComponent<Collider2D>().enabled = false;

        GameplayManagers.Instance.Fade.FadeGameObjectOut(gameObject, _destroyTime, null);
        Destroy(gameObject, _destroyTime);
    }

    void SwitchToDestroyedVisuals()
    {
        GetComponent<SpriteRenderer>().sprite = _destroyedVisuals;
    }
}
