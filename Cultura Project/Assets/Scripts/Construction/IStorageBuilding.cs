using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    public interface IStorageBuilding : IBuilding
    {
        int AdditionalWoodStorage { get; set; }
        int AdditionalStoneStorage { get; set; }
        int AdditionalMetalStorage { get; set; }
        int AdditionalFoodStorage { get; set; }
    }
}