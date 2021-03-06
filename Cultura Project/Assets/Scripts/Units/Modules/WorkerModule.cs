﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units.Modules
{
    public class WorkerModule : MonoBehaviour, IVillagerModule
    {
        public int ID
        {
            get
            {
                return (int)ModuleID.Worker;
            }
        }

        private VillagerBase villagerBase;

        public void Initialize(VillagerBase villagerBase)
        {
            this.villagerBase = villagerBase;
        }

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