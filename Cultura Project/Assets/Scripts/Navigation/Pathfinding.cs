using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Cultura.Core;

namespace Cultura.Navigation
{
    [RequireComponent(typeof(Grid))]
    public class Pathfinding : MonoBehaviour
    {
        private Grid grid;

        [SerializeField]
        private Transform startPos;

        [SerializeField]
        private Transform endPos;

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FindPath(startPos.position, endPos.position);
        }

        public void FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Node startNode = grid.NodeFromWorldPoint(startPosition);
            Node targetNode = grid.NodeFromWorldPoint(targetPosition);

            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("pathFound : " + sw.ElapsedMilliseconds + " ms");
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Node neighbour in grid.GetNeighours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                    int newMovementCost = GetDistance(currentNode, neighbour) + currentNode.gCost;
                    bool neighbourWithinOpenSet = openSet.Contains(neighbour);
                    if (newMovementCost < neighbour.gCost || !neighbourWithinOpenSet)
                    {
                        neighbour.gCost = newMovementCost;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!neighbourWithinOpenSet)
                        {
                            openSet.Add(neighbour);
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            grid.path = path;
        }

        private int GetDistance(Node nodeA, Node nodeB)
        {
            int distanceX = Mathf.Abs(nodeB.gridX - nodeA.gridX);
            int distanceY = Mathf.Abs(nodeB.gridY - nodeA.gridY);

            if (distanceX > distanceY)
                return (14 * distanceY) + (10 * (distanceX - distanceY));
            else
                return (14 * distanceX) + (10 * (distanceY - distanceX));
        }
    }
}