using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAnimComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootAnimationCompleted()
    {
        GameplayManagers.Instance.Ball.LaunchBall();
    }
}
