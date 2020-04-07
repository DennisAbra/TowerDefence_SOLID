using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    private List<Vector2Int> walkableTiles = new List<Vector2Int>();

    public Vector2Int EndTile { get; private set; }
    public Vector2Int StartTile { get; private set; }

    public List<Vector2Int> SpawnTile(int x, int y, int value, Tile[] tiles, Transform parent)
    {
        foreach (Tile t in tiles)
        {
            if ((int)t.tileType == value)
            {
                Vector3 newPos = Vector3.right * x * t.displacement + Vector3.forward * y * t.displacement;
                Instantiate(t.prefabToSpawn, newPos, Quaternion.identity, parent.transform);
                if (t.isWalkable)
                {
                    walkableTiles.Add(new Vector2Int(x,y));

                    if ((int)t.tileType == value && value == (int)TypeOfTile.StartTile)
                    {
                        StartTile = new Vector2Int(x,y);
                    }
                    if ((int)t.tileType == value && value == (int)TypeOfTile.EndTile)
                    {
                        EndTile = new Vector2Int(x, y);
                    }
                }
            }
        }
        return walkableTiles;
    }
}
