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

    // Update is called once per frame
    void Update()
    {

    }

    //Function to destroy the ball when it collides with the killbox
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox"))
        {
            BallController.GetComponent<BallSpawner>().BallsInScene.Remove(GetComponent<BallPhysics>());
            if (BallController.GetComponent<BallSpawner>().BallsInScene.Count <= 0)
            {
                BallController.GetComponent<BallSpawner>().SetButtonInactive();
            }
            Destroy(gameObject);
            BallController.GetComponent<BallSpawner>().SetButtonActive();
            
        }

        if(collision.gameObject.CompareTag("Splitter"))
        {
            BallController.GetComponent<BallSpawner>().BallSplit(transform.position, collision.gameObject);
        }
    }

    
}
