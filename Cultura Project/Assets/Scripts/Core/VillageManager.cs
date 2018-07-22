using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public class VillageManager : MonoBehaviour
    {
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

        /// <summary>
        /// Fires an event when there is a need for more units or when that need is satisfied
        /// <para b> test</para>
        /// </summary>
        public event Action<bool> UnitCountEventHandler;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}