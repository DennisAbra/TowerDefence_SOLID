using UnityEngine;
using Tools;
using System.Collections.Generic;

public class Tower : MonoBehaviour, IDamageDealer
{
    [SerializeField] private UnitData data;
    [SerializeField] private uint bulletPoolSize;

    private GameObjectPool bulletPool;
    private GameObject target;
    private bool lostTarget = true;


    private void Start()
    {
        bulletPool = new GameObjectPool(bulletPoolSize, data.Prefab, bulletPoolSize, new GameObject(gameObject.name + "'s BulletPool").transform);
    }

    private void Update()
    {
        if (target)
        {
            if (target.gameObject.activeSelf == false)
                target = null;

        }
    }

    public void DealDamage(IDamagable thingToDamage)
    {
        thingToDamage.TakeDamage(data.Damage);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<IDamagable>(out IDamagable currentTarget))
        {
            if (lostTarget)
            {
                target = col.gameObject;
                Debug.Log("GOT HERE");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
            lostTarget = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (target)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }

}
