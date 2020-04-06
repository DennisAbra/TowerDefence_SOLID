using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapSpawner))]
public class MapManager : MonoBehaviour
{
    [SerializeField] Tile[] tiles;
    [SerializeField] TextAsset mapToRead;
    [SerializeField] MapReader mapReader;
    [SerializeField] Transform parent;
    MapSpawner spawner;

    int[,] map;

    void Awake()
    {
        if (parent == null)
        {
            parent = new GameObject("MapObjects").transform;
            parent.position = Vector3.zero;
        }

        if (mapReader == null)
        {
            mapReader = new MapReader();
        }
        if(spawner == null)
        {
            spawner = GetComponent<MapSpawner>();
        }

        map = mapReader.ReadLevelMap(mapToRead);

       // int numOfDimension = map.Rank;
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                spawner.SpawnTile(x, y, map[x, y], tiles, parent);
            }
        }
    }
}
