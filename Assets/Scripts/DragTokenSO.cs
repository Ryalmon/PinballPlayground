using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DragToken", menuName = "ScriptableObjects/DragToken", order = 2)]
public class DragTokenSO : ScriptableObject
{
    public GameObject _objectToSpawn;
    public Sprite _tokenVisuals;
}
