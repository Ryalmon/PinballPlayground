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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillBox")
        {
            Destroy(gameObject);
            BallController.GetComponent<BallSpawner>().BallIsInGame = false;
        }
    }
}
