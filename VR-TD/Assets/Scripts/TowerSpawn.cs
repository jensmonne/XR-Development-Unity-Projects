using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    // Prefab for the archer tower to spawn
    [SerializeField] private GameObject archerTowerPrefab;

    // Prefab for the mage tower to spawn
    [SerializeField] private GameObject mageTowerPrefab;

    // Trigger event that detects when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone is tagged as "ArcherTower"
        if (other.CompareTag("ArcherTower"))
        {
            // Spawn an archer tower and destroy the triggering object
            SpawnArcherTower();
            Destroy(other.gameObject);
        }

        // Check if the object entering the trigger zone is tagged as "MageTower"
        if (other.CompareTag("MageTower"))
        {
            // Spawn a mage tower and destroy the triggering object
            SpawnMageTower();
            Destroy(other.gameObject);
        }
    }

    // Method to spawn an archer tower at the current position
    private void SpawnArcherTower()
    {
        Instantiate(archerTowerPrefab, transform.position, Quaternion.identity);
    }

    // Method to spawn a mage tower at the current position
    private void SpawnMageTower()
    {
        Instantiate(mageTowerPrefab, transform.position, Quaternion.identity);
    }
}