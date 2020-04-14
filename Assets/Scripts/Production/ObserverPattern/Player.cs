using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] int m_InitHealth;

    public ObservableProperty<int> Health { get; } = new ObservableProperty<int>();
    public int CurrentHealth { get; set; }

    public void TakeDamage(int damage)
    {
        Health.Value -= damage;
    }

    private void Awake()
    {
        Health.Value = m_InitHealth;
    }
}
