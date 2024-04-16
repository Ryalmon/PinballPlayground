using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnSplitter : MonoBehaviour
{
    [SerializeField] GameObject SplitterPrefab;
    [SerializeField] float XRight;
    [SerializeField] float YUpperSpawnBounds;
    [SerializeField] float XLeft;
    [SerializeField] float YLowerSpawnBounds;
    [SerializeField] float SpawnCountdown;
    [SerializeField] float _TwoBallCooldown;
    [SerializeField] float _ThreeBallCooldown;
    private float _currentBallCooldown;
    private float currentTime;
    private float PosOrNeg;
    // Start is called before the first frame update
    void Start()
    {
        //currentTime = SpawnCountdown;
        _currentBallCooldown = SpawnCountdown;
        AssignEvents();
    }

    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetGameStartEvent().AddListener(StartSplitterCreation);
    }

    void CreateSplitter()
    {

        GameObject splitter = Instantiate(SplitterPrefab, RandomizeSpawnLocation(),Quaternion.identity);
        Vector3 MoveDirection = new Vector3 ((transform.position.x * PosOrNeg),0,0).normalized;
        splitter.GetComponent<MovingObjects>().SetMoveDirection(MoveDirection);
    }

    Vector2 RandomizeSpawnLocation()
    {
        //This decides the X value where it spawns
        Vector2 SpawnPoint;
        int i = Random.Range(0, 2);
        if (i == 1)
        {
            //SplitterSpawnPoints[0] = XLeft;
            SpawnPoint = new Vector2 (XLeft, Random.Range(YUpperSpawnBounds, YLowerSpawnBounds));
            PosOrNeg = -1;
        }
        else
        {
            //SplitterSpawnPoints[0] = XRight;
            SpawnPoint = new Vector2(XRight, Random.Range(YUpperSpawnBounds, YLowerSpawnBounds));
            PosOrNeg = 1;
        }
        //this decides the random Y value where it spawns
        //SplitterSpawnPoints[1] = ;
        return SpawnPoint;

    }

    public void StartSplitterCreation()
    {
        StartCoroutine(SplitterCreationCooldown());
    }

    public float CooldownBasedOnBallsInScene()
    {
        int _ballsInScene = GameplayManagers.Instance.Ball.GetBallsInSceneCount();
        if (_ballsInScene >= 3)
            return _ThreeBallCooldown;
        else if (_ballsInScene == 2)
            return _TwoBallCooldown;
        else
            return SpawnCountdown;
    }

    private IEnumerator SplitterCreationCooldown()
    {
        currentTime = CooldownBasedOnBallsInScene();
        while (GameplayManagers.Instance.State.GPS == GameStateManager.GamePlayState.Play)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0.0f)
            {
                currentTime = CooldownBasedOnBallsInScene();
                CreateSplitter();
            }
            yield return null;
        }
    }

}
