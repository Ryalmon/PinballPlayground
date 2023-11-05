using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] GameObject pointParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Testing Point Particles, will remove later
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector2 end = new Vector2(2, 4);
            StartCoroutine(SpawnPointParticles(Vector2.zero, end,8));
        }
    }

    public IEnumerator SpawnPointParticles(Vector2 startPos, Vector2 endPos, float particlesSpawned)
    {
        List<GameObject> particleList = new List<GameObject>();
        for(int i = 0; i < particlesSpawned; i++)
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            GameObject currentParticle = Instantiate(pointParticle, startPos, Quaternion.identity);
            currentParticle.GetComponent<PointParticle>().AssignStartValues(dir, endPos);
            particleList.Add(currentParticle);
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.1f);
        foreach(GameObject particle in particleList)
        {
            PointParticle pScript = particle.GetComponent<PointParticle>();
            pScript.StartCoroutine(pScript.MoveTowards());
        }


    }
}
