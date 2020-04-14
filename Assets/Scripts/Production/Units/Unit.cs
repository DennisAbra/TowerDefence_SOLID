﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : PathAgent, IDamageDealer, IDamagable
{
    [SerializeField] UnitData unitData;
    Animator animator;
    Player player;
    public int CurrentHealth { get; set; }

    private void OnEnable()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }

        Reset();
        CurrentHealth = unitData.MaxHp;
        Speed = unitData.MoveSpeed;
        animator.SetBool("isWalking", true);
    }

    void Update()
    {
        Move();
        if(IsOnEndTile())
        {
            DealDamage(player);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        animator.SetTrigger("Damaged");

        if (CurrentHealth <= 0)
        {
            //DIE
            animator.SetTrigger("Killed");
            gameObject.SetActive(false);
        }
    }

    public void DealDamage(IDamagable thingToDamage)
    {
        thingToDamage.TakeDamage(unitData.Damage);
    }
}

