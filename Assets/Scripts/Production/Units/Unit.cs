using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : PathAgent, IDamageDealer, IDamagable
{
    [SerializeField] UnitData unitData;
    public int CurrentHealth { get; set; }

    private void OnEnable()
    {
        Reset();
        CurrentHealth = unitData.MaxHp;
        Speed = unitData.MoveSpeed;
    }

    void Update()
    {
        Move();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 1)
        {
            //DIE
        }
    }

    public void DealDamage(IDamagable thingToDamage)
    {
        thingToDamage.TakeDamage(unitData.Damage);
    }
}

