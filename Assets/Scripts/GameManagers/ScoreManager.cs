using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Ball Point Scaling")]
    [SerializeField] float _startingBallMultiplier;
    [SerializeField] float _ballMultiplierScalingRate;
    [SerializeField] float _ballMultiplerScalingAmount;
    float _currentBallMultiplier;
    Coroutine _scalingCoroutine;

    internal int CurrentScore;
    
    // Start is called before the first frame update
    void Start()
    {
        SetStartingScale();
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
        while (true)
        {
            yield return new WaitForSeconds(_ballMultiplierScalingRate);
            SetCurrentMultiplier(_currentBallMultiplier + _ballMultiplerScalingAmount);
            ScalingUIUpdate();
        }
    }

    private void ScalingUIUpdate()
    {
        GameplayParent.Instance.UI.UpdateMultiplierUI(_currentBallMultiplier);
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
    public void CreatePointParticles(GameObject spawnSource, int score)
    {
        VFXManager vfxMan = UniversalManager.Instance.VFX;
        vfxMan.StartCoroutine(vfxMan.SpawnPointParticles(spawnSource,GameplayParent.Instance.UI.GetScoreTextLocation(),ScoreTimesBallMultiplier(score)));
    }

    private int ScoreTimesBallMultiplier(int score)
    {
        return Mathf.CeilToInt(score * GetBallLifetimeMultiplier());
    }

    #endregion

    public void AddToScore(int addedScore)
    {
        CurrentScore += addedScore;
        UpdateText();
    }

    private void UpdateText()
    {
        //Displays the score in the game world
        GameplayParent.Instance.UI.UpdateScoreUI(CurrentScore);
        //_scoreText.text = CurrentScore.ToString();
    }
}
