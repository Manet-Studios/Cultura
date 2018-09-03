using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction.Modules
{
    [RequireComponent(typeof(Collider2D))]
    public class BuildingBase : MonoBehaviour
    {
        private Dictionary<int, IBuildingModule> modules = new Dictionary<int, IBuildingModule>();

        public void Build()
        {
            IBuildingModule[] buildingModules = GetComponents<IBuildingModule>();
            for (int i = 0; i < buildingModules.Length; i++)
            {
                modules.Add(buildingModules[i].ID, buildingModules[i]);
            }
        }

        public void Demolish()
        {
            foreach (KeyValuePair<int, IBuildingModule> item in modules)
            {
                item.Value.OnDemolish();
            }
        }
    }
}