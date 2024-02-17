using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayParent : MonoBehaviour
{
    public static GameplayParent Instance;
    public TimerManager Timer;
    public GameStateManager State;
    public ScoreManager Score;
    public FlipperManager Flippers;
    public GameUIManager UI;
    public BallSpawner Ball;
    public SpawningObjects Spawning;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
}
