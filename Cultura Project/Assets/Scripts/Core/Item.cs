using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public enum ItemType { Material, Consumable }

    public class Item
    {
        public string name;
        public string description;
        public int id;
        public virtual ItemType ItemType { get { return ItemType.Material; } }
        public Sprite icon;
    }

    public class Food : Item
    {
        public int satiationLevel;
        public override ItemType ItemType { get { return ItemType.Consumable; } }
    }
}