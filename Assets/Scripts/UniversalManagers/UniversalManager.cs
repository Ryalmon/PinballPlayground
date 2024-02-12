using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allManagers;
    public InputManager Input;
    public SaveManager Save;
    public SceneLoadingManager Scene;
    public VFXManager VFX;

    public static UniversalManager Instance;


    private void Awake()
    {
        EstablishSingleton();
        SpawnManagers();
        
    }

    private void EstablishSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void SpawnManagers()
    {
        //Spawns all prefabs in the _allManagers list
        foreach (GameObject m in _allManagers)
        {
            Instantiate(m, transform.position, transform.rotation).transform.parent = gameObject.transform;
        }
        AssignManagers();
    }

    void AssignManagers()
    {
        Input = FindObjectOfType<InputManager>();
        Save = FindObjectOfType<SaveManager>();
        Scene = FindObjectOfType<SceneLoadingManager>();
        VFX = FindObjectOfType<VFXManager>();
    }
}
