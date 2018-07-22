using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField]
        private int woodSupplyLevel;

        [SerializeField]
        private int stoneSupplyLevel;

        [SerializeField]
        private int metalSupplyLevel;

        [SerializeField]
        private int foodSupplyLevel;

        [SerializeField]
        private int contentnessLevel;

        #region Properties

        public int WoodSupplyLevel
        {
            get
            {
                return woodSupplyLevel;
            }

            set
            {
                woodSupplyLevel = value;

                if (UpdateWoodSupplyLevelEventHandler != null) UpdateWoodSupplyLevelEventHandler(woodSupplyLevel);
            }
        }

        public int StoneSupplyLevel
        {
            get
            {
                return stoneSupplyLevel;
            }

            set
            {
                stoneSupplyLevel = value;
                if (UpdateStoneSupplyLevelEventHandler != null) UpdateStoneSupplyLevelEventHandler(stoneSupplyLevel);
            }
        }

        public int MetalSupplyLevel
        {
            get
            {
                return metalSupplyLevel;
            }

            set
            {
                metalSupplyLevel = value;
                if (UpdateMetalSupplyLevelEventHandler != null) UpdateMetalSupplyLevelEventHandler(metalSupplyLevel);
            }
        }

        public int FoodSupplyLevel
        {
            get
            {
                return foodSupplyLevel;
            }

            set
            {
                foodSupplyLevel = value;
                if (UpdateFoodSupplyLevelEventHandler != null) UpdateFoodSupplyLevelEventHandler(foodSupplyLevel);
            }
        }

        public int ContentnessLevel
        {
            get
            {
                return contentnessLevel;
            }

            set
            {
                contentnessLevel = value;
                if (UpdateContentnessLevelEventHandler != null) UpdateContentnessLevelEventHandler(contentnessLevel);
            }
        }

        #endregion Properties

        #region Event Handlers

        public event Action<int> UpdateWoodSupplyLevelEventHandler;

        public event Action<int> UpdateStoneSupplyLevelEventHandler;

        public event Action<int> UpdateMetalSupplyLevelEventHandler;

        public event Action<int> UpdateFoodSupplyLevelEventHandler;

        public event Action<int> UpdateContentnessLevelEventHandler;

        #endregion Event Handlers
    }
}