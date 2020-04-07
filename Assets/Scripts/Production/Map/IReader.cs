using UnityEngine;

public interface IReader
{
    (int[,], Vector2Int[]) ReadMap(TextAsset textMap);
}