using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawnables = new List<GameObject>();
    
    private void ObjectsSpawn()
    {
        Instantiate(Spawnables[0], new Vector2(-7, 3), Quaternion.identity);
    }

    private void Start()
    {
        ObjectsSpawn();
    }
}
