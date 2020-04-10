using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    List<Vector2Int> walkableTiles { get; set; }
    void Spawn();
}