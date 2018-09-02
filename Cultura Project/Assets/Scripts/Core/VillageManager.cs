using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public class VillageManager : SerializedMonoBehaviour
    {
        public static VillageManager Instance;

        public static Registry RegistryInstance;

        [SerializeField]
        private Registry registry;

        [NonSerialized]
        [OdinSerialize]
        public Inventory inventory;

        [SerializeField]
        private int unitCount;

        [SerializeField]
        private int unitCapacity;

        private bool requireAdditionalUnits = false;

        public int UnitCount
        {
            get
            {
                return unitCount;
            }

            set
            {
                unitCount = Mathf.Min(value, UnitCapacity);

                if (UnitCountEventHandler != null)
                {
                    if (requireAdditionalUnits != unitCount < UnitCapacity)
                    {
                        requireAdditionalUnits = unitCount < UnitCapacity;
                        UnitCountEventHandler(requireAdditionalUnits);
                    }
                }
            }
        }

        public int UnitCapacity
        {
            get
            {
                return unitCapacity;
            }

            set
            {
                unitCapacity = value;

                if (UnitCountEventHandler != null)
                {
                    if (requireAdditionalUnits != UnitCount < UnitCapacity)
                    {
                        requireAdditionalUnits = UnitCount < UnitCapacity;
                        UnitCountEventHandler(requireAdditionalUnits);
                    }
                }
            }
        }

        public event Action<bool> UnitCountEventHandler;

        private void Awake()
        {
            Instance = this;
            RegistryInstance = registry;
        }
    }
}