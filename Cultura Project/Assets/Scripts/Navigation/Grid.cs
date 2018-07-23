using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Cultura.Navigation
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private LayerMask unwalkableLayers;

        public Vector2Int worldSize;

        public float nodeRadius;

        private Node[,] grid;

        private float nodeDiameter;
        private List<Tilemap> walkableTilemaps;

        public List<Node> path = new List<Node>();

        public bool displayExclusivelyPathGizmos;

        public int MaxSize
        {
            get
            {
                return worldSize.x * worldSize.y;
            }
        }

        private void Start()
        {
            nodeDiameter = nodeRadius * 2f;
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            walkableTilemaps = new List<Tilemap>();
            walkableTilemaps.AddRange(FindObjectsOfType<Tilemap>());
            List<Tilemap> unwalkableTilemaps = new List<Tilemap>();
            unwalkableTilemaps.AddRange(walkableTilemaps.Where(t => unwalkableLayers == (unwalkableLayers | (1 << t.gameObject.layer))));
            grid = new Node[worldSize.x, worldSize.y];

            Vector2 bottomLeft = (Vector2)transform.position - ((Vector2)worldSize * .5f);

            for (int x = 0; x < worldSize.x; x++)
            {
                for (int y = 0; y < worldSize.y; y++)
                {
                    Vector3 point = bottomLeft + new Vector2(x, y);
                    Vector3Int gridPoint = new Vector3Int();
                    bool walkable = walkableTilemaps.Any(t =>
                    {
                        gridPoint = t.WorldToCell(point);
                        return !unwalkableTilemaps.Any(u => u.HasTile(gridPoint)) && t.HasTile(gridPoint);
                    });
                    grid[x, y] = new Node(walkable, new Vector2Int(gridPoint.x, gridPoint.y), x, y);
                }
            }
        }

        public Node NodeFromWorldPoint(Vector2 position)
        {
            Vector2 bottomLeft = (Vector2)transform.position - ((Vector2)worldSize * .5f);

            Vector3Int gridPos = walkableTilemaps[0].WorldToCell(position);
            float percentX = Mathf.Clamp01((gridPos.x - bottomLeft.x) / worldSize.x);
            float percentY = Mathf.Clamp01((gridPos.y - bottomLeft.y) / worldSize.y);

            int x = Mathf.Clamp((int)(percentX * (worldSize.x - 1)) + 1, 0, worldSize.x - 1);
            int y = Mathf.Clamp((int)(percentY * (worldSize.y - 1)) + 1, 0, worldSize.y - 1);
            return grid[x, y];
        }

        public List<Node> GetNeighours(Node centerNode)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) continue;

                    int checkX = centerNode.gridX + x;
                    int checkY = centerNode.gridY + y;

                    if ((checkX > -1 && checkX < worldSize.x) &&
                        (checkY > -1 && checkY < worldSize.y))
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, worldSize.y));

            if (!displayExclusivelyPathGizmos)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (grid != null)
                {
                    Node playerNode = NodeFromWorldPoint(mousePos);

                    foreach (Node n in grid)
                    {
                        Gizmos.color = n.walkable ? Color.green : Color.red;

                        if (path != null)
                        {
                            if (path.Contains(n))
                                Gizmos.color = Color.black;
                        }
                        Gizmos.DrawCube(new Vector3(n.worldPosition.x, n.worldPosition.y, 0), Vector3.one * (nodeDiameter - .1f));
                    }

                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(mousePos, .1f);

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(new Vector3(playerNode.worldPosition.x, playerNode.worldPosition.y, 0), .25f);
                }
            }
            else
            {
                if (path != null)
                {
                    for (int i = 0; i < path.Count; i++)
                    {
                        Gizmos.DrawCube(new Vector3(path[i].worldPosition.x, path[i].worldPosition.y, 0), Vector3.one * (nodeDiameter - .1f));
                    }
                }
            }
        }
    }
}