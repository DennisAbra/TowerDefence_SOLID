using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    private List<Vector3> walkableTiles = new List<Vector3>();

    public List<Vector3> WalkableTiles{ get; private set; }

    public void SpawnTile(int x, int y, int value, Tile[] tiles, Transform parent)
    {
        foreach(Tile t in tiles)
        {
            if((int)t.tileType == value)
            {
                Vector3 newPos = Vector3.right * x * t.displacement + Vector3.forward * y * t.displacement;
                Instantiate(t.prefabToSpawn, newPos, Quaternion.identity, parent.transform);
                if(t.isWalkable)
                {
                  //  Debug.Log(newPos);
                    walkableTiles.Add(newPos);
                }
            }
        }
        WalkableTiles = walkableTiles;
    }
}
