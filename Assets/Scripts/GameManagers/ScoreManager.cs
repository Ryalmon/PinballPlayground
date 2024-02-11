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
        _currentBallMultiplier = _startingBallMultiplier;
    }

    #region BallPointMultiplier
    public void StartScaling()
    {
        SetCurrentMultiplier(_startingBallMultiplier);
        _scalingCoroutine = StartCoroutine(ScalingProcess());
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
        }
    }

    public float GetBallLifetimeMultiplier()
    {
        return _currentBallMultiplier;
    }
    #endregion

    #region PointParticles
    public void CreatePointParticles(GameObject spawnSource, int score)
    {
        VFXManager vfxMan = UniversalManager.Instance.VFX;
        vfxMan.StartCoroutine(vfxMan.SpawnPointParticles(spawnSource,GameplayParent.Instance.UI.GetScoreTextLocation(),score));
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
