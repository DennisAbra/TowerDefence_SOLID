using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    static List<Vector3> walkablePositions;
    public GameObject prefab;

    void Start()
    {
        //Gets all walkable tiles in order. 
        //Start on the first element End on the last
        walkablePositions = FindObjectOfType<MapSpawner>().WalkableTiles;

    }

    Vector3 CalculateNextTile(Vector3Int currentPos)
    {
        //Find next tile to move to
        int indexOfNextTile = walkablePositions.IndexOf(currentPos) + 1;

        return walkablePositions[indexOfNextTile];
    }

    void MoveToNextTile()
    {
        //Check if next tile is occupied.
        // if not move to it
    }

}