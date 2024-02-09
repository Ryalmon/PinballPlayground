using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawnables = new List<GameObject>();
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    
    private void ObjectsSpawn()
    {
        for (int i = 0; i < Spawnables.Count; i++)
        {
            //Random.Range(i, Spawnables.Count);
            GameObject gameObject = Spawnables[Random.Range(0, Spawnables.Count)];
            //Instantiate(Spawnables[Random.Range(i, Spawnables.Count)], SpawnPoints[i].position, Quaternion.identity);
            Instantiate(gameObject, SpawnPoints[i].position, Quaternion.identity);
        }
    }

  

    private void Start()
    {
        ObjectsSpawn();
    }
}
