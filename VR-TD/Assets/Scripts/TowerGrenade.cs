using UnityEngine;

public class TowerGrenade : MonoBehaviour
{
    // Prefab for the tower to spawn upon grenade collision with terrain
    public GameObject towerPrefab;

    // Called when this object collides with another
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Terrain"
        if (collision.gameObject.CompareTag("Terrain"))
        {
            // Instantiate a tower at the grenade's current position
            Instantiate(towerPrefab, transform.position, Quaternion.identity);

            // Destroy the grenade object after spawning the tower
            Destroy(gameObject);
        }
    }
}