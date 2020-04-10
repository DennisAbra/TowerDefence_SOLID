public interface IDamagable
{
    int CurrentHealth { get; set; }

    void TakeDamage(int damage);
}