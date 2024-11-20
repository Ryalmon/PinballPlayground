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

    private PointParticle[] _pointParticlesPool;
    private const int POINT_PARTICLES_POOL_COUNT = 600;
    private int _particlePoolCounter = 0;

    private void Awake()
    {
        EstablishPointParticlePool();
    }

    private void EstablishPointParticlePool()
    {
        _pointParticlesPool = new PointParticle[POINT_PARTICLES_POOL_COUNT];
        for(int i = 0; i < POINT_PARTICLES_POOL_COUNT; i++)
        {
            GameObject newParticle = Instantiate(_pointParticle, transform.position, Quaternion.identity);
            _pointParticlesPool[i] = newParticle.GetComponent<PointParticle>();
            newParticle.SetActive(false);
            ObjectPoolingParent.Instance.AddObjectAsChild(newParticle);
        }
    }

    private PointParticle GetNextPointParticleInPool()
    {
        int currentCounter = _particlePoolCounter;
        _particlePoolCounter++;
        if (_particlePoolCounter >= POINT_PARTICLES_POOL_COUNT)
            _particlePoolCounter = 0;

        return _pointParticlesPool[currentCounter];
    }

    public IEnumerator SpawnPointParticles(GameObject spawnSource, Vector2 endPos, int score)
    {
        Vector3 spawnSourceLoc = spawnSource.transform.position;
        int particleNumber = DetermineParticleNum(score);
        PointParticle[] particleArray = new PointParticle[particleNumber];
        for (int i = 0; i < particleNumber; i++)
        {
            //Spawns a new particle
            PointParticle newestPoint;
            if(spawnSource == null)
                newestPoint = SpawnPointGameObject(spawnSourceLoc);
            else
            {
                newestPoint = SpawnPointGameObject(spawnSource.transform.position);
                spawnSourceLoc = spawnSource.transform.position;
            }
                
            //Decrements total score and assigns the score to the point particle
            int newPointValue = IndividualPointValue(score);
            //Debug.Log(newPointValue);
            newestPoint.SetPointValue(newPointValue);
            score -= newPointValue;
            //Adds the most recent particle to a list
            particleArray[i] = newestPoint;

            //Waits to spawn another particle
            yield return new WaitForSeconds(_pointPSpawnDelay);
        }
        //After all particles have spawned wait for a set time
        yield return new WaitForSeconds(.1f);
        foreach(PointParticle particle in particleArray)
        {
            //Makes all particles that were spawned, start their MoveTowards function
            particle.StartMoveTowards(endPos);
            yield return new WaitForSeconds(_pointPMoveDelay);
        }
    }

    private PointParticle SpawnPointGameObject(Vector2 spawnPos)
    {
        //Choose a random direction
        Vector2 dir = Random.insideUnitCircle.normalized;
        //Creates the point particle
        PointParticle currentParticle = GetNextPointParticleInPool() ;
        currentParticle.gameObject.SetActive(true);
        currentParticle.gameObject.transform.position = spawnPos;
        currentParticle.StartMoveAway(dir);

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
