using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject archerTowerPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArcherTower"))
        {
            SpawnArcherTower();
            Destroy(other.gameObject);
        }
    }

    private void SpawnArcherTower()
    {
        Instantiate(archerTowerPrefab, transform.position, Quaternion.identity);
    }
}
