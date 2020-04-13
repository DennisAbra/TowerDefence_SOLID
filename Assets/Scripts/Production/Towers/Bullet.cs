using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IShooter
{
    [SerializeField] float bulletSpeed;
    public float ProjectileSpeed { get; set; }
    float t = 0;

    private void OnEnable()
    {
        ProjectileSpeed = bulletSpeed; 
        t = 0;
    }

    public void Shoot(Vector3 target)
    {
        Vector3 cachedPos = transform.position;
        t += Time.deltaTime * ProjectileSpeed;
        transform.position = Vector3.Lerp(cachedPos, target, t);
    }
}
