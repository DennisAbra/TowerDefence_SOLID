using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapReader : IReader
{
    string[] mapData;
    string[] rows;
    int[,] map;
    Vector2Int[] waveData;

    public (int[,], Vector2Int[]) ReadMap(TextAsset textMap)
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

        string[] unitData = mapData[1].Split('\n');
        waveData = new Vector2Int[unitData.Length-1];
        int counter = 0;
        int index = 0;
        for (int i = 1; i < unitData.Length; i++)
        {
            string[] units = unitData[i].Split(' ');
            foreach (var u in units)
            {
                if (counter == 0)
                {
                    waveData[index].x = int.Parse(u);
                    counter = 1;
                }
                else if (counter == 1)
                {
                    waveData[index].y = int.Parse(u);
                    counter = 0;
                    index++;
                }
            }
        }

        return (map, waveData);
    }
}
