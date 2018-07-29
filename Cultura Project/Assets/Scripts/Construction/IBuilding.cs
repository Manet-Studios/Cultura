using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    public interface IBuilding
    {
        ConstructionCosts ConstructionCost { get; set; }
        BuildingBlueprint BlueprintPrefab { get; }

        void OnBuild();

        void OnDemolish();
    }
}