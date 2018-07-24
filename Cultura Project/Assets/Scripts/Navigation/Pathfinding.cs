using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Cultura.Core;
using System;

namespace Cultura.Navigation
{
    [RequireComponent(typeof(PathRequestManager))]
    [RequireComponent(typeof(Grid))]
    public class Pathfinding : MonoBehaviour
    {
        private PathRequestManager pathRequestManager;
        private Grid grid;

        private void Awake()
        {
            grid = GetComponent<Grid>();
            pathRequestManager = GetComponent<PathRequestManager>();
        }

        private IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Vector2[] waypoints = new Vector2[0];
            bool pathSuccess = false;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Node startNode = grid.NodeFromWorldPoint(startPosition);
            Node targetNode = grid.NodeFromWorldPoint(targetPosition);

            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            if (startNode.walkable && targetNode.walkable)
            {
                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        sw.Stop();
                        print("pathFound : " + sw.ElapsedMilliseconds + " ms");
                        pathSuccess = true;
                        break;
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

            yield return null;

            if (pathSuccess) waypoints = RetracePath(startNode, targetNode);

            pathRequestManager.FinishedProcessingPath(waypoints, pathSuccess);
        }

        public void StartFindPath(Vector3 pathStart, Vector3 pathEnd)
        {
            StartCoroutine(FindPath(pathStart, pathEnd));
        }

        private Vector2[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            Vector2[] waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            return waypoints;
        }

        private Vector2[] SimplifyPath(List<Node> path)
        {
            List<Vector2> waypoints = new List<Vector2>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);

                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i - 1].worldPosition);
                }

                directionOld = directionNew;
            }

            return waypoints.ToArray();
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