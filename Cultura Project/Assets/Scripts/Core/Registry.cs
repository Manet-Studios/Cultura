using Cultura.Construction;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    [CreateAssetMenu(menuName = "Cultura/Registry")]
    public class Registry : SerializedScriptableObject
    {
        public Dictionary<int, Item> ItemRegistry = new Dictionary<int, Item>();
        public Dictionary<int, BuildingBlueprint> BuildingRegistry = new Dictionary<int, BuildingBlueprint>();
    }
}