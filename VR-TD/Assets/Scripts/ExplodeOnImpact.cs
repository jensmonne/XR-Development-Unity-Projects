using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public int damageAmount = 100;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
            Damage(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Damage(GameObject enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}