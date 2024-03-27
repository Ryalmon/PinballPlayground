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
            UniversalManager.Instance.Sound.PlaySFX("Death");
            GameplayManagers.Instance.Ball.RemoveBall(gameObject);
        }
    }

    
}
