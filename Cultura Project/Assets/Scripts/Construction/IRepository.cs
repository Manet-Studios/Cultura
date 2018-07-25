using Cultura.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    public interface IRepository
    {
        Inventory Inventory { get; }
    }
}