using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawnables = new List<GameObject>();

    [SerializeField] List<DragTokenSO> Placeables = new List<DragTokenSO>();
    [SerializeField] GameObject _placementToken;

    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<GameObject> SpawnedObjects = new List<GameObject>();
    [SerializeField] float _respawnObjectDelay;

    List<GameObject> _spawnPointsUsedBeforeGameStart = new List<GameObject>();
    private void Start()
    {
        AssignEvents();
        ObjectsSpawn(); 
    }

    private void AssignEvents()
    {
        GameplayManagers.Instance.State.GetGameStartEvent().AddListener(ActivatePregameList);
    }

    //Creates placeables at game start
    private void ObjectsSpawn()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            GameObject newGameObj = CreateSpawnedObj(i);
            SpawnedObjects.Add(newGameObj);
        }
    }

    //Creates placeable to replace ones that were placed
    public void SpawnNewObject(GameObject oldObject)
    {
        int index = SpawnedObjects.IndexOf(oldObject);
        if (index != -1)
        {
            GameObject newGameObj = CreateSpawnedObj(index);
            SpawnedObjects[index] = newGameObj;
        }
    }

    //Creates the placeable object at the correct spawn point
    private GameObject CreateSpawnedObj(int index)
    {
        GameObject newGameObj = Instantiate(_placementToken, SpawnPoints[index].position, Quaternion.identity);
        newGameObj.GetComponent<DragnDrop>().AssignPlacementData(Placeables[Random.Range(0, Placeables.Count)]);
        return newGameObj;
    }


    //If an object was placed before the game started keep track of which ones
    private void AddOldObjToPreGameList(GameObject oldObject)
    {
        _spawnPointsUsedBeforeGameStart.Add(oldObject);
    }

    //When the game starts tell all spawn points to start their cooldown
    private void ActivatePregameList()
    {
        foreach(GameObject currentOldObj in _spawnPointsUsedBeforeGameStart)
        {
            StartSpawnDelay(currentOldObj);
        }
    }

    //Determines if the item was placed before the game started or after
    public void PlaceableObjectPlaced(GameObject placed)
    {
        switch(GameplayManagers.Instance.State.GPS)
        {
            case (GameStateManager.GamePlayState.Intro):
                AddOldObjToPreGameList(placed);
                return;
            case (GameStateManager.GamePlayState.Play):
                StartSpawnDelay(placed);
                return;
        }
    }

    //Activates the SpawnDelay couroutine
    public void StartSpawnDelay(GameObject oldObject)
    {
        StartCoroutine(SpawnDelay(oldObject));
    }

    //Waits for a set amount of time before placing a placeable object
    private IEnumerator SpawnDelay(GameObject oldObject)
    {
        yield return new WaitForSeconds(_respawnObjectDelay);
        SpawnNewObject(oldObject);
    }
}
