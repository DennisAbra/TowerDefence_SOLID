using UnityEngine;
using Tools;
using System.Collections.Generic;

public class Tower : MonoBehaviour, IDamageDealer
{
    [SerializeField] private UnitData data;
    [SerializeField] private uint bulletPoolSize;
    [SerializeField] private float timeUntillNextShot = 1;
    [SerializeField] private float timeFraction = .1f;

    private GameObjectPool bulletPool;
    private GameObject target;
    private bool lostTarget = true;
    private float t = 0;

    private void Start()
    {
        bulletPool = new GameObjectPool(bulletPoolSize, data.Prefab, bulletPoolSize, new GameObject(gameObject.name + "'s BulletPool").transform);
    }

    private void Update()
    {
        t += Time.deltaTime * timeFraction;
        if (target)
        {
            if (target.gameObject.activeSelf == false)
            {
                target = null;
            }
            else if(t > timeUntillNextShot && target)
            {
                GameObject instance = bulletPool.Rent(true);
                instance.transform.position = transform.position;
                Bullet b = instance.GetComponent<Bullet>();
                b.Target = target;
                t = 0;
            }

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
