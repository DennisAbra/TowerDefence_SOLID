using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSpawner : MonoBehaviour
{
    [SerializeField] UnitData unitData;
    [SerializeField] int amountToSpawn;

    Queue<GameObject> units = new Queue<GameObject>();

    void CreateUnits()
    {
        GameObject go = Instantiate(unitData.unitPrefab);
        units.Enqueue(go);
    }
}

