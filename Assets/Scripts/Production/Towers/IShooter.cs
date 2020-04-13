using UnityEngine;

public interface IShooter
{
    float ProjectileSpeed { get; set; }
    void Shoot(Vector3 target);
}

