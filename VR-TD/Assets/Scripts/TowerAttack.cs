using UnityEngine;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour
{
    // Rate of fire (shots per second)
    public float fireRate = 1f;

    // Prefab for the projectile to be fired
    public GameObject projectilePrefab;

    // The point from which projectiles will be fired
    public Transform firePoint;

    // Time when the next shot can be fired
    private float nextFireTime = 0f;

    // List of enemies currently within the tower's range
    private List<Transform> enemiesInRange = new List<Transform>();

    // Triggered when an object enters the tower's range
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Add the enemy to the list of enemies in range
            enemiesInRange.Add(other.transform);
        }
    }

    // Triggered when an object exits the tower's range
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Remove the enemy from the list of enemies in range
            enemiesInRange.Remove(other.transform);
        }
    }

    // Returns the closest enemy within range
    Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        // Iterate through all enemies in range to find the closest one
        foreach (Transform enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                // Calculate the distance to this enemy
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);

                // Check if this enemy is closer than the current closest enemy
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
            else
            {
                // Remove null references from the list
                enemiesInRange.Remove(enemy);
                closestEnemy = null;
                return closestEnemy;
            }
        }

        return closestEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed to fire the next shot
        if (Time.time >= nextFireTime)
        {
            // Find the closest enemy
            Transform target = GetClosestEnemy();

            // If there's a valid target, shoot at it
            if (target != null)
            {
                Shoot(target);

                // Set the time for the next shot
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    // Fires a projectile at the specified target
    void Shoot(Transform target)
    {
        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Set the target for the projectile
        projectile.GetComponent<Projectile>().SetTarget(target);
    }
}