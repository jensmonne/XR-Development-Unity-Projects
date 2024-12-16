using UnityEngine;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private float nextFireTime = 0f;
    private List<Transform> enemiesInRange = new List<Transform>();
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
    
    Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        // Loop door alle enemies die in de buurt zijn
        foreach (Transform enemy in enemiesInRange)
        {
            // Check dat de enemy nog bestaat (niet al dood is!)
            if(enemy != null)
            {
                // Check de afstand tussen toren en enemy
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
                // Bewaar de kortste afstand om de dichtsbijzijnde enemy te vinden
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            } else
            {
                // Als de enemy dood gegaan is in range van de toren, verwijder hem dan uit de lijst
                // We passen de lijst nu aan en eindigen de loop
                enemiesInRange.Remove(enemy);
                closestEnemy = null;
                return closestEnemy;
            }
        }
        return closestEnemy;
    }
    
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Transform target = GetClosestEnemy();
            if (target != null)
            {
                Shoot(target);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(target);
    }
}