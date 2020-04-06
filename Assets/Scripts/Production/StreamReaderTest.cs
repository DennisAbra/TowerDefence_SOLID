using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class StreamReaderTest : MonoBehaviour
{
    [SerializeField] Tile[] tiles;
    [SerializeField] TextAsset textMap;

    string[] mapData;
    string[] rows;

    int[,] map;
    GameObject parent;
    private List<Vector2Int> walkableTiles = new List<Vector2Int>();
     
    void Awake()
    {
        if (parent == null)
        {
            parent = new GameObject();
            parent.name = "WorldObjects";

        }

     mapData = textMap.text.Split('#');   
        CreateMap(mapData);

        string[] unitData = mapData[1].Split('\n');

        foreach (string ud in unitData)
        {
            if (ud.Length > 2)
            {
                //  Shows waves like this
                // 10 1
                CreateNewWave(ud);
            }
        }
    }

    private void CreateMap(string[] mapData)
    {
        rows = mapData[0].Split('\n');

        map = new int[rows[0].Length, rows.Length];
        for (int y = 0; y < rows.Length; y++)
        {
            char[] rowChars = new char[rows[y].Length];
            rowChars = rows[y].ToCharArray();
            for (int x = 0; x < rowChars.Length; x++)
            {
                map[x, y] = (int)char.GetNumericValue(rowChars[x]);
                SpawnTile(x, y, map[x, y]);
            }
        }
    }


    private void CreateNewWave(string ud)
    {
        string[] temp = ud.Split(' ');
        //temp[0] number of units type 1
        //temp[1] number of units type 2
    }

    private void SpawnTile(int x, int y, int v)
    {
        foreach (Tile t in tiles)
        {
            if ((int)t.tileType == v)
            {
                Vector3 newPos = Vector3.right * x * t.displacement + Vector3.forward * y * t.displacement;
                Instantiate(t.prefabToSpawn, newPos, Quaternion.identity, parent.transform);
                if (t.isWalkable)
                {
                    walkableTiles.Add(new Vector2Int((int)newPos.x, (int)newPos.y));
                }
                break;
            }
        }
    }
}
