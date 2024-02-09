using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PointsDebug()
    {
        UniversalManager.Instance.VFX.StartCoroutine(UniversalManager.Instance.VFX.SpawnPointParticles(Vector2.zero, new Vector2(0, 10), 8));
    }

    public void StartGame()
    {
        GameplayParent.Instance.State.StartGame();
    }

    public void DisplayScores()
    {
        foreach(ScoreBoardText score in FindObjectsOfType<ScoreBoardText>())
        {
            score.ChangeText();
        }
    }
}
