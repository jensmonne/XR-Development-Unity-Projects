using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Speed of the projectile in units per second
    public float speed = 10f;

    // Damage dealt to the target upon impact
    public float damage = 50f;

    // Target transform the projectile is moving towards
    private Transform target;

    // Method to set the target for the projectile
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the projectile if there is no target
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate the normalized direction vector towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the projectile towards the target
        transform.position += direction * (speed * Time.deltaTime);

        // Rotate the projectile to face the target
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Adjust the rotation to ensure the projectile faces correctly
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 180f, transform.eulerAngles.z);

        // Check if the projectile is close enough to the target to trigger an explosion
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Explode();
        }
    }

    // Method to handle the explosion logic
    void Explode()
    {
        // Check if the target has a Health component and apply damage
        if (target.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }

        // Destroy the projectile after the explosion
        Destroy(gameObject);
    }
}