using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace AI
{
    //TODO: Implement IPathFinder using Dijsktra algorithm.
    public class Dijkstra : IPathFinder
    {
        private List<Vector2Int> grid = new List<Vector2Int>();
        private Queue<Vector2Int> frontier = new Queue<Vector2Int>();

        private Dictionary<Vector2Int, Vector2Int> ancestors = new Dictionary<Vector2Int, Vector2Int>();
        private List<Vector2Int> neighbours = new List<Vector2Int>();

        private Vector2Int currentNode;
        private List<Vector2Int> path = new List<Vector2Int>();

        public Dijkstra(List<Vector2Int> accessibleNodes)
        {
            grid = accessibleNodes;
        }

        public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            currentNode = start;
            frontier.Enqueue(currentNode);

            while (frontier.Count > 0)
            {
                currentNode = frontier.Dequeue();
                if (currentNode == goal)
                {
                    break;
                }

                foreach (Vector2Int n in GetNeighbours())
                {
                    if (grid.Contains(n))
                    {
                        if (!ancestors.ContainsKey(n))
                        {
                            frontier.Enqueue(n);
                            ancestors.Add(n, currentNode);
                        }
                    }
                }
            }

            if (!ancestors.ContainsKey(goal))
            {
                return null;
            }

            if (ancestors.ContainsKey(goal))
            {
                foreach (KeyValuePair<Vector2Int, Vector2Int> a in ancestors)
                {
                    path.Add(a.Value);
                }
            }
            path.Reverse();
            return path;
        }

        List<Vector2Int> GetNeighbours()
        {
            neighbours.Clear();
            neighbours.Add(currentNode + Vector2Int.up);
            neighbours.Add(currentNode + Vector2Int.right);
            neighbours.Add(currentNode + Vector2Int.down);
            neighbours.Add(currentNode + Vector2Int.left);
            return neighbours;
        }

    }
}
