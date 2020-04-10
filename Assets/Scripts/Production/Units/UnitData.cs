using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField]  GameObject unitPrefab;
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHp;
    [SerializeField] int damage;

    public GameObject UnitPrefab { get => unitPrefab; }
    public float MoveSpeed { get => moveSpeed;}
    public int MaxHp { get => maxHp; }
    public int Damage { get => damage; }

}
