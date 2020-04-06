using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/UnitData")]
public class UnitData : ScriptableObject
{
    public readonly GameObject unitPrefab;
    public readonly float moveSpeed;
    public readonly int maxHp;
    public readonly int damage;
}
