using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 50f;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the projectile towards the target
        transform.position += direction * (speed * Time.deltaTime);
        
        

        // Rotate the projectile to face the direction it is moving
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 180f, transform.eulerAngles.z);

        // Check if the projectile is close enough to the target to "explode"
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (target.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}