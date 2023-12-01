using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allManagers;

    private void Awake()
    {
        //Spawns all prefabs in the _allManagers list
        foreach (GameObject m in _allManagers)
        {
            Instantiate(m, transform.position, transform.rotation);
        }
        //Destroys itself
        Destroy(gameObject);
    }
}
