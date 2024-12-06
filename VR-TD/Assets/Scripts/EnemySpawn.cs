using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public int maxEnemiesToSpawn;
    public Transform player;
    private float minSpawnRadius = 8f;

    private void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            minSpawnRadius -= 0.3f;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        maxEnemiesToSpawn += 1;
        for (int i = 0; i < maxEnemiesToSpawn; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minSpawnRadius, spawnRadius);

            float x = player.position.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = player.position.z + distance * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 spawnPosition = new Vector3(x, player.position.y - 0.5f, z);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}