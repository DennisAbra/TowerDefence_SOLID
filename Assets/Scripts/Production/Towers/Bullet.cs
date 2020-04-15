using UnityEngine;
using Tools;

public class Bullet : MonoBehaviour, IElemental
{
    enum ElementalType { Ice, Fire };

    [SerializeField] float bulletSpeed;
    [SerializeField] float explosionRadius = 1;
    [SerializeField] int damage;
    [SerializeField] ElementalType type;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject VFX;

    public float ProjectileSpeed { get; set; }
    public GameObject Target { get; set; }
    float t = 0;

    GameObjectPool explosionPool;

    private void Start()
    {
        explosionPool = new GameObjectPool(5, VFX, 5, new GameObject("ExplosionPool").transform);
    }

    private void Update()
    {
        t += Time.deltaTime * bulletSpeed;
        if (Target)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, t);
            if (t > 1) t = 0;
        }
        else gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ElementalEffect(collision.gameObject);
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public void ElementalEffect(GameObject go)
    {
        if (type == ElementalType.Fire)
        {
            GameObject instance =  explosionPool.Rent(true);
            instance.transform.position = transform.position;
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius, mask);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<IDamagable>().TakeDamage(damage);
            }
        }
        if (type == ElementalType.Ice)
        {
            go.GetComponent<Unit>().isSlowed = true;
        }
    }
}

public interface IElemental
{
    void ElementalEffect(GameObject go);
}
