using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawnables = new List<GameObject>();
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<GameObject> SpawnedObjects = new List<GameObject>();
    [SerializeField] float _respawnObjectDelay;
    private void Start()
    {
        ObjectsSpawn(); 
    }
    private void ObjectsSpawn()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            GameObject gameObject = Spawnables[Random.Range(0, Spawnables.Count)];
            GameObject newObject = Instantiate(gameObject, SpawnPoints[i].position, Quaternion.identity);
            SpawnedObjects.Add(newObject);
        }
    }

    public void SpawnNewObject(GameObject oldObject)
    {
        int index = SpawnedObjects.IndexOf(oldObject);
        if (index != -1)
        {
            GameObject newGameObject = Instantiate(Spawnables[Random.Range(0,Spawnables.Count)], SpawnPoints[index].position, Quaternion.identity);
            SpawnedObjects[index] = newGameObject;
        }
    }

    public void StartSpawnDelay(GameObject oldObject)
    {
        StartCoroutine(SpawnDelay(oldObject));
    }

    private IEnumerator SpawnDelay(GameObject oldObject)
    {
        yield return new WaitForSeconds(_respawnObjectDelay);
        SpawnNewObject(oldObject);
    }
}
