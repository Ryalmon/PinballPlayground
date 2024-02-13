using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Point Particles")]
    [SerializeField] float _pointPSpawnDelay;
    [SerializeField] float _pointPMoveDelay;
    //[SerializeField] float _pointMoveAwayDistance;
    [SerializeField] int _basePointValue;
    [SerializeField] GameObject _pointParticle;

    public IEnumerator SpawnPointParticles(GameObject spawnSource, Vector2 endPos, int score)
    {
        List<GameObject> particleList = new List<GameObject>();
        int particleNumber = DetermineParticleNum(score);
        for (int i = 0; i < particleNumber; i++)
        {
            //Spawns a new particle
            GameObject newestPoint = SpawnPointGameObject(spawnSource.transform.position);
            //Decrements total score and assigns the score to the point particle
            int newPointValue = IndividualPointValue(score);
            Debug.Log(newPointValue);
            newestPoint.GetComponent<PointParticle>().SetPointValue(newPointValue);
            score -= newPointValue;
            //Adds the most recent particle to a list
            particleList.Add(newestPoint);

            //Waits to spawn another particle
            yield return new WaitForSeconds(_pointPSpawnDelay);
        }
        //After all particles have spawned wait for a set time
        yield return new WaitForSeconds(.1f);
        foreach(GameObject particle in particleList)
        {
            //Makes all particles that were spawned, start their MoveTowards function
            PointParticle pScript = particle.GetComponent<PointParticle>();
            pScript.StartCoroutine(pScript.MoveTowards(endPos));
            yield return new WaitForSeconds(_pointPMoveDelay);
        }
    }

    private GameObject SpawnPointGameObject(Vector2 spawnPos)
    {
        //Choose a random direction
        Vector2 dir = Random.insideUnitCircle.normalized;
        //Creates the point particle
        GameObject currentParticle = Instantiate(_pointParticle, spawnPos, Quaternion.identity);
        //Passes the direction and endPos values into the most recent particle
        PointParticle pScript = currentParticle.GetComponent<PointParticle>();
        pScript.StartCoroutine(pScript.MoveAway(dir));

        return currentParticle;
    }

    private int DetermineParticleNum(int score)
    {
        return Mathf.CeilToInt((float)score / (float)_basePointValue);
    }

    private int IndividualPointValue(int score)
    {
        if (score > _basePointValue)
            return _basePointValue;
        return score;
    }
}
