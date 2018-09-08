using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cultura.Construction.Modules
{
    [RequireComponent(typeof(Collider2D))]
    public class BuildingBase : MonoBehaviour
    {
        private Dictionary<int, IBuildingModule> modules = new Dictionary<int, IBuildingModule>();

        private List<DemoCondition> demolishConditions = new List<DemoCondition>();

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
            DemoCondition condition = demolishConditions.FirstOrDefault(d => !d.condition(this));
            if (condition != null)
            {
                //TODO
                return;
            }

            foreach (KeyValuePair<int, IBuildingModule> item in modules)
            {
                item.Value.OnDemolish();
            }

            Destroy(gameObject);
        }

        private T GetModule<T>(int buildingID) where T : IBuildingModule
        {
            if (!modules.ContainsKey(buildingID))
            {
                return default(T);
            }

            return (T)modules[buildingID];
        }

        public void AddDemolitionCondition(DemoCondition condition)
        {
            if (!demolishConditions.Contains(condition))
            {
                demolishConditions.Add(condition);
            }
        }

        public void RemoveDemolitionCondition(DemoCondition condition)
        {
            if (demolishConditions.Contains(condition))
            {
                demolishConditions.Remove(condition);
            }
        }
    }

    [System.Serializable]
    public class DemoCondition
    {
        public string message;
        public Predicate<BuildingBase> condition;

        public DemoCondition(string message, Predicate<BuildingBase> condition)
        {
            this.message = message;
            this.condition = condition;
        }
    }
}