using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using Tools;

public class UnitManager : MonoBehaviour, ISpawner
{
    //Change this to a non static / private variable
    public static List<Vector3> walkablePositions = new List<Vector3>();
    MapManager mapManager;


    [SerializeField] GameObject smallUnit;
    [SerializeField] GameObject largeUnit;
    [SerializeField] uint poolSize;

    GameObjectPool smallUnitPool;
    GameObjectPool largeUnitPool;

    float timeUntillNextSpawn;
    float t = 0;
    int index = 0;

    public List<Vector2Int> walkableTiles { get; set; }

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

        smallUnitPool = new GameObjectPool(poolSize, smallUnit, poolSize, new GameObject("Small Units").transform);
        largeUnitPool = new GameObjectPool(poolSize, largeUnit, poolSize, new GameObject("Large Units").transform);

        GameObject instance = smallUnitPool.Rent(true);
        timeUntillNextSpawn = instance.GetComponent<PathAgent>().Speed * 0.5f;
        mapManager.WaveData[0].x -= 1;
        instance.transform.position = walkablePositions[0];
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    public void Spawn()
    {
        t += timeUntillNextSpawn * Time.deltaTime;
        if (t > 2)
        {
            if (mapManager.WaveData[index].x > 0)
            {
                GameObject instance = smallUnitPool.Rent(true);
                mapManager.WaveData[index].x -= 1;
                instance.transform.position = walkablePositions[0];
            }
            else if (mapManager.WaveData[index].y > 0)
            {
                GameObject instance = largeUnitPool.Rent(true);
                mapManager.WaveData[index].y -= 1;
                instance.transform.position = walkablePositions[0];
            }
            else if (index < mapManager.WaveData.Length - 1) // Current wave is finished spawning. Add a wait time and indication that there will be another round comming
            {
                index++;
                //If index > WaveData.Count all waves have finished
            }
            t = 0;
        }
    }
}
