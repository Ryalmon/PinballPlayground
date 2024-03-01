using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplit : MonoBehaviour
{
    [SerializeField] GameObject BallPrefab;
    private GameObject ballController;
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        ballController = GameObject.FindGameObjectWithTag("BallController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ball = collision.gameObject;
            SplitBall();
            Destroy(gameObject);
            //THIS SOUND MANAGER LINE IS CREATING ERRORS PLEASE FIX ALEX
            //SoundManager.Instance.PlaySFX("Laser 2");
            //ballController.GetComponent<BallSpawner>().BallSplit(transform.position, collision.gameObject);
        }
    }

    void SplitBall()
    {
        Instantiate(BallPrefab, ball.transform.position, Quaternion.identity);
    }
}
