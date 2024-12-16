using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    public float explosionForce = 500f;      // Kracht van de explosie
    public float explosionRadius = 5f;       // Radius van de explosie

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Zorg dat vijanden de tag "Enemy" hebben
        {
            Explode();
            Destroy(gameObject); // Verwijder het object na de explosie
        }
    }

    private void Explode()
    {
        // Vind alle objecten in de buurt van de explosie
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