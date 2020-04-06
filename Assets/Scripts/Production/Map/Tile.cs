using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/Tile")]
public class Tile : ScriptableObject
{
    public TypeOfTile tileType;
    public GameObject prefabToSpawn;
    public bool isWalkable;
    public float displacement;

    //public TileData CreateTileData(Vector2Int position)
    //{
    //    TileData tData = new TileData(position, displacement, tileType, isWalkable, prefabToSpawn);
    //    return tData;
    //}
    
}

public enum TypeOfTile
{
    PathTile = 0,
    ObstacleTile = 1,
    TowerDef1 = 2,
    TowerDef2 = 3,
    StartTile = 8,
    EndTile = 9
}

//public struct TileData
//{
//    public TypeOfTile type;
//    public Vector2Int position;
//    public float displacement;
//    public bool isWalkable;
//    public GameObject prefab;

//    public TileData(Vector2Int pos, float displacement, TypeOfTile tile, bool walkable, GameObject prefabToSpawn)
//    {
//        position = pos;
//        this.displacement = displacement;
//        type = tile;
//        isWalkable = walkable;
//        prefab = prefabToSpawn;
//    }
//}
