using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAnimComplete : MonoBehaviour
{
    public void ShootAnimationCompleted()
    {
        GameplayManagers.Instance.Ball.LaunchBall();
    }
}
