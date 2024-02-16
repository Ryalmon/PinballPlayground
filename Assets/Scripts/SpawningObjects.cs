using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawnables = new List<GameObject>();
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<GameObject> SpawnedObjects = new List<GameObject>();
    private void Start()
    {
        ObjectsSpawn();
        
    }
    private void ObjectsSpawn()
    {
        for (int i = 0; i < Spawnables.Count; i++)
        {
            GameObject gameObject = Spawnables[Random.Range(0, Spawnables.Count)];
            GameObject newObject = Instantiate(gameObject, SpawnPoints[i].position, Quaternion.identity);
            SpawnedObjects.Add(newObject);
            //Instantiate(gameObject, SpawnPoints[i].position, Quaternion.identity);
        }

    }

    public void SpawnNewObject(GameObject oldObject)
    {
        int index = SpawnedObjects.IndexOf(oldObject);
        if (index != -1)
        {
            GameObject newGameObject = Instantiate(Spawnables[index], SpawnPoints[index].position, Quaternion.identity);
            SpawnedObjects[index] = newGameObject;
        }
    }

 

}
