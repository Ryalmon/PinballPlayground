using UnityEngine;

[CreateAssetMenu(fileName = "DragToken", menuName = "ScriptableObjects/DragToken", order = 2)]
public class DragTokenSO : ScriptableObject
{
    public GameObject _objectToSpawn;
    public Sprite _tokenVisuals;
}
