using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("Point Particles")]
    [SerializeField] float _pointPSpawnDelay;
    [SerializeField] float _pointPMoveDelay;
    [SerializeField] GameObject _pointParticle;

    public static VFXManager M_Instance;

    void Awake()
    {
        EstablishSingleton();
    }

    private void EstablishSingleton()
    {
        if (M_Instance != null && M_Instance != this)
        {
            Destroy(gameObject);
        }

        M_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Testing Point Particles, will remove later
        if (Input.GetKeyDown(KeyCode.A))
        {
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
            GameObject currentParticle = Instantiate(_pointParticle, startPos, Quaternion.identity);
            currentParticle.GetComponent<PointParticle>().AssignStartValues(dir, endPos);
            particleList.Add(currentParticle);
            yield return new WaitForSeconds(_pointPSpawnDelay);
        }
        yield return new WaitForSeconds(.1f);
        foreach(GameObject particle in particleList)
        {
            PointParticle pScript = particle.GetComponent<PointParticle>();
            pScript.StartCoroutine(pScript.MoveTowards());
            yield return new WaitForSeconds(_pointPMoveDelay);
        }


    }
}
