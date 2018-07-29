using Cultura.Construction;
using Cultura.Units;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cultura/Registry")]
public class Registry : SerializedScriptableObject
{
    private Dictionary<int, IBuilding> buildings = null;
    public Dictionary<int, GameObject> buildingPrefabs = new Dictionary<int, GameObject>();

    public Dictionary<int, GameObject> units = new Dictionary<int, GameObject>();

    public IBuilding GetBuilding(int key)
    {
        if (buildings == null)
        {
            buildings = new Dictionary<int, IBuilding>();
            foreach (var pair in buildingPrefabs)
            {
                buildings.Add(pair.Key, pair.Value.GetComponent<IBuilding>());
            }
        }

        return buildings[key];
    }
}