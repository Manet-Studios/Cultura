using Cultura.Construction;
using Cultura.Units;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    [CreateAssetMenu(menuName = "Cultura/Registry")]
    public class Registry : SerializedScriptableObject
    {
        public Dictionary<int, Item> ItemRegistry = new Dictionary<int, Item>();
        public Dictionary<int, IBuilding> BuildingRegistry = new Dictionary<int, IBuilding>();
    }
}