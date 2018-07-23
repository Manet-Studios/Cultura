using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Core;

namespace Cultura.Navigation
{
    public class Node : IHeapItem<Node>
    {
        public bool walkable;
        public Vector2Int worldPosition;
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;

        private int heapIndex;

        public Node parent;

        public int FCost { get { return gCost + hCost; } }

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }

            set
            {
                heapIndex = value;
            }
        }

        public Node(bool walkable, Vector2Int worldPosition, int gridX, int gridY)
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        public int CompareTo(Node nodeToCompare)
        {
            int compare = FCost.CompareTo(nodeToCompare.FCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }

            return -compare;
        }
    }
}