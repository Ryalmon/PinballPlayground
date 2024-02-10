using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Point Particles")]
    [SerializeField] float _pointPSpawnDelay;
    [SerializeField] float _pointPMoveDelay;
    [SerializeField] GameObject _pointParticle;

    /*// Update is called once per frame
    void Update()
    {
        //Testing Point Particles, will remove later
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 end = new Vector2(0, 4.5f);
            StartCoroutine(SpawnPointParticles(Vector2.zero, end,8));
        }
    }*/



    public IEnumerator SpawnPointParticles(GameObject spawnSource, Vector2 endPos, int score)
    {
        List<GameObject> particleList = new List<GameObject>();
        for(int i = 0; i < DetermineParticleNum(score); i++)
        {
            //Adds the most recent particle to a list
            particleList.Add(SpawnPointGameObject(spawnSource.transform.position));
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
        return 5;
    }
}
