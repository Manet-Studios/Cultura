using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units
{
    public interface IVillager
    {
        int CurrentHealth { get; set; }
        int MaximumHealth { get; set; }

        float CurrentProductivity { get; set; }
        float MaximumProductivity { get; set; }

        int HungerLevel { get; set; }
        int MaximumHungerLevel { get; set; }

        float CurrentHappinessLevel { get; set; }
        float MaximumHappinessLevel { get; set; }

        float CurrentEnergyLevel { get; set; }
        float MaximumEnergyLevel { get; set; }

        void OnSpawn();

        void OnDie();
    }
}