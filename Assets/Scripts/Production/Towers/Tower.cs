using UnityEngine;
using Tools;
using System.Collections.Generic;

public class Tower : MonoBehaviour, IDamageDealer, ITower
{
    [SerializeField] private UnitData data;
    [SerializeField] private uint bulletPoolSize;

    private GameObjectPool bulletPool;
    private bool isAiming = false;
    private GameObject target;


    private void Start()
    {
        bulletPool = new GameObjectPool(bulletPoolSize, data.Prefab, bulletPoolSize, new GameObject(gameObject.name + "'s BulletPool").transform);
    }

    public void DealDamage(IDamagable thingToDamage)
    {
        thingToDamage.TakeDamage(data.Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable currentTarget) && !isAiming)
        {
            target = other.gameObject;
            isAiming = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            isAiming = false;
            target = null;
        }
    }

}
