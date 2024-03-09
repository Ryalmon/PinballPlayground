using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManagers : MonoBehaviour
{
    public static GameplayManagers Instance;
    public TimerManager Timer;
    public GameStateManager State;
    public ScoreManager Score;
    public FlipperManager Flippers;
    public GameUIManager UI;
    public BallSpawner Ball;
    public SpawningObjects Spawning;
    public ObjectFadingManager Fade;
    public SpawnSplitter Splitter;
    public PlacementLocation PlaceArea;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
