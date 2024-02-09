using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Point Particles")]
    [SerializeField] float _pointPSpawnDelay;
    [SerializeField] float _pointPMoveDelay;
    [SerializeField] GameObject _pointParticle;

    // Update is called once per frame
    void Update()
    {
        //Testing Point Particles, will remove later
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 end = new Vector2(0, 4.5f);
            StartCoroutine(SpawnPointParticles(Vector2.zero, end,8));
        }
    }

    public IEnumerator SpawnPointParticles(Vector2 startPos, Vector2 endPos, float particlesSpawned)
    {
        List<GameObject> particleList = new List<GameObject>();
        for(int i = 0; i < particlesSpawned; i++)
        {
            //Choose a random direction
            Vector2 dir = Random.insideUnitCircle.normalized;
            //Creates the point particle
            GameObject currentParticle = Instantiate(_pointParticle, startPos, Quaternion.identity);
            //Passes the direction and endPos values into the most recent particle
            currentParticle.GetComponent<PointParticle>().AssignStartValues(dir, endPos);
            //Adds the most recent particle to a list
            particleList.Add(currentParticle);
            //Waits to spawn another particle
            yield return new WaitForSeconds(_pointPSpawnDelay);
        }
        //After all particles have spawned wait for a set time
        yield return new WaitForSeconds(.1f);
        foreach(GameObject particle in particleList)
        {
            //Makes all particles that were spawned, start their MoveTowards function
            PointParticle pScript = particle.GetComponent<PointParticle>();
            pScript.StartCoroutine(pScript.MoveTowards());
            yield return new WaitForSeconds(_pointPMoveDelay);
        }


    }
}
