using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKillBox : MonoBehaviour
{
    public GameObject BallController;

    // Start is called before the first frame update
    void Start()
    {
        BallController = GameObject.FindGameObjectWithTag("BallController");
    }

    //Function to destroy the ball when it collides with the killbox
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox"))
        {
            GameplayManagers.Instance.Ball.RemoveBall(gameObject);
            /*BallController.GetComponent<BallSpawner>().BallsInScene.Remove(GetComponent<BallPhysics>());
            BallController.GetComponent<BallSpawner>().CheckBallCountIsZero();
            Destroy(gameObject);*/
            /*if (BallController.GetComponent<BallSpawner>().BallsInScene.Count <= 0)
            {
                BallController.GetComponent<BallSpawner>().SetButtonInactive();
            }
            Destroy(gameObject);
            BallController.GetComponent<BallSpawner>().SetButtonActive();*/
        }

        //if(collision.gameObject.CompareTag("Splitter"))
        //{
        //    BallController.GetComponent<BallSpawner>().BallSplit(transform.position, collision.gameObject);
        //}
    }

    
}
