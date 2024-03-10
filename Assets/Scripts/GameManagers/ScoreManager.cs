using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Value From Sources")]
    [SerializeField] int _scoreFromBumper;
    [SerializeField] int _scoreFromSpaceShip;
    [SerializeField] int _scoreFromBlackHole;
    [SerializeField] int _scoreFromCeiling;
    
    [Space]

    [Header("Ball Point Scaling")]
    [SerializeField] float _startingBallMultiplier;
    [SerializeField] float _ballMultiplierScalingRate;
    [SerializeField] float _ballMultiplerScalingAmount;
    [SerializeField] float _ballMultiplierMax;
    float _currentBallMultiplier;
    Coroutine _scalingCoroutine;

    internal int CurrentScore;
    Dictionary<ScoreSource, int> _scoreDictionary = new Dictionary<ScoreSource, int>()
    {
        { ScoreSource.Bumper,0},
        { ScoreSource.SpaceShip,0},
        { ScoreSource.BlackHole,0},
        { ScoreSource.Ceiling,0}
    };

    

    // Start is called before the first frame update
    void Start()
    {
        SetStartingScale();
        PopulateDictionary();
        AssignEvents();
    }

    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetBallActiveEvent().AddListener(StartScaling);
        GameplayManagers.Instance.State.GetBallDeactiveEvent().AddListener(StopScaling);
    }

    void PopulateDictionary()
    {
        _scoreDictionary[ScoreSource.Bumper] = _scoreFromBumper;
        _scoreDictionary[ScoreSource.SpaceShip] = _scoreFromSpaceShip;
        _scoreDictionary[ScoreSource.BlackHole] = _scoreFromBlackHole;
        _scoreDictionary[ScoreSource.Ceiling] = _scoreFromCeiling;
    }

    #region BallPointMultiplier
    public void StartScaling()
    {
        SetCurrentMultiplier(_startingBallMultiplier);
        _scalingCoroutine = StartCoroutine(ScalingProcess());
        ScalingUIUpdate();
    }

    private void SetStartingScale()
    {
        SetCurrentMultiplier(_startingBallMultiplier);
    }

    private void SetCurrentMultiplier(float _newMultiplier)
    {
        _currentBallMultiplier = _newMultiplier;
    }

    IEnumerator ScalingProcess()
    {
        while (_currentBallMultiplier < _ballMultiplierMax- _ballMultiplerScalingAmount)
        {
            yield return new WaitForSeconds(_ballMultiplierScalingRate);
            SetCurrentMultiplier(_currentBallMultiplier + _ballMultiplerScalingAmount);
            ScalingUIUpdate();
        }
    }

    private void ScalingUIUpdate()
    {
        GameplayManagers.Instance.UI.UpdateMultiplierUI(_currentBallMultiplier);
    }

    public float GetBallLifetimeMultiplier()
    {
        return _currentBallMultiplier;
    }

    public void StopScaling()
    {
        StopCoroutine(_scalingCoroutine);
        SetStartingScale();
    }
    #endregion

    #region PointParticles
    public void CreatePointParticles(GameObject spawnSource, ScoreSource source)
    {
        //Starts the spawn point particles coroutine on the vfx manager
        //Spawnsource is where to spawn them
        //GetScoreTextLocation is where to send them
        //ScoreTimesBallMultipler returns the score multiplied by the current multipler
        //ScoreValueFromSource gets the score based on the source of what called it.
        VFXManager vfxMan = UniversalManager.Instance.VFX;
        vfxMan.StartCoroutine(vfxMan.SpawnPointParticles(spawnSource, GameplayManagers.Instance.UI.GetScoreTextLocation(),
            ScoreTimesBallMultiplier(ScoreValueFromSource(source))));
    }

    private int ScoreTimesBallMultiplier(int score)
    {
        return Mathf.CeilToInt(score * GetBallLifetimeMultiplier());
    }

    #endregion

    public void AddToScore(int addedScore)
    {
        CurrentScore += addedScore;
        //Displays the score in the game world
        GameplayManagers.Instance.UI.UpdateScoreUI(CurrentScore,addedScore);
    }

/*    private void UpdateScoreUI()
    {
        
        //_scoreText.text = CurrentScore.ToString();
    }*/

    private int ScoreValueFromSource(ScoreSource source)
    {
        return _scoreDictionary[source];
    }

    public float GetStartingMultiplier()
    {
        return _startingBallMultiplier;
    }

    public float GetMaxMultiplier()
    {
        return _ballMultiplierMax;
    }
}

public enum ScoreSource
{
    Bumper,
    SpaceShip,
    BlackHole,
    Ceiling
};