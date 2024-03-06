using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blacklist", menuName = "ScriptableObjects/Blacklist",order = 1)]
public class NameBlacklistSO : ScriptableObject
{
    public List<string> _blackListedNames;
}
