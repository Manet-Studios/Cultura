using Cultura.Construction;
using Cultura.Units;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cultura/Registry")]
public class PoolableObjectRegistry : SerializedScriptableObject
{
    public Dictionary<int, GameObject> buildings = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> units = new Dictionary<int, GameObject>();
}