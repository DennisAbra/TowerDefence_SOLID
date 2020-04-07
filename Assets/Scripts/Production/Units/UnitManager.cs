using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class UnitManager : MonoBehaviour
{
    static List<Vector2Int> walkableTiles;
    public static List<Vector3> walkablePositions = new List<Vector3>();
    MapManager mapManager;


    public GameObject smallUnit;
    public GameObject largeUnit;


    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        walkableTiles = mapManager.WalkableTiles;
        IPathFinder pathFinder = new Dijkstra(walkableTiles);
        
        
        if (pathFinder != null)
        {
            IEnumerable<Vector2Int> path = pathFinder.FindPath(mapManager.StartTile, mapManager.EndTile);
            if (path != null)
            {
                foreach (var p in path)
                {
                    walkablePositions.Add(Vector3.right * p.x * 2 + Vector3.forward * p.y * 2 + Vector3.up * 0.5f);
                }
            }
            else Debug.Log("No path found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnUnits()
    {
        //Contains what units to spawn. 0 for first wave and LastIndex for last wave 
        //mapManager.WaveData
    }
}
