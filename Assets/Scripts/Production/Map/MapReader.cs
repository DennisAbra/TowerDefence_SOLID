using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapReader
{
    string[] mapData;
    string[] rows;
    int[,] map;

    public int[,] ReadLevelMap(TextAsset textMap)
    {
        mapData = textMap.text.Split('#');
        rows = mapData[0].Split('\n');

        map = new int[rows[0].Length, rows.Length];
        for (int y = 0; y < rows.Length; y++)
        {
            char[] rowChars = new char[rows[y].Length];
            rowChars = rows[y].ToCharArray();
            for (int x = 0; x < rowChars.Length; x++)
            {
                map[x, y] = (int)char.GetNumericValue(rowChars[x]);
            }
        }
        return map;
    }
}
