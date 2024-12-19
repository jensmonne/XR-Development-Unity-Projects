using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // References to enemy prefabs to spawn
    [SerializeField] private GameObject Mage;
    [SerializeField] private GameObject Tank;
    [SerializeField] private GameObject Spearman;
    [SerializeField] private GameObject Archer;

    // Interval in seconds between spawns
    public float spawnInterval = 5f;

    // Tracks the last time an enemy was spawned
    private float lastSpawnTime;

    private void Update()
    {
        // Check if the spawn interval has elapsed since the last spawn
        if (Time.time >= lastSpawnTime + spawnInterval)
        {
            // Spawn a random enemy
            SpawnEnemies();

            // Update the last spawn time to the current time
            lastSpawnTime = Time.time;
        }
    }

    // Spawns enemies based on a random value and their probabilities
    private void SpawnEnemies()
    {
        // Generate a random float between 0 and 1
        float randomValue = Random.Range(0f, 1f);

        // Spawn a Tank if random value is <= 0.2 and Tank prefab is assigned
        if (randomValue <= 0.2f && Tank != null)
        {
            Instantiate(Tank, transform.position, transform.rotation);
        }
        // Spawn a Spearman if random value is <= 0.5 and Spearman prefab is assigned
        else if (randomValue <= 0.5f && Spearman != null)
        {
            Instantiate(Spearman, transform.position, transform.rotation);
        }
        // Spawn an Archer if random value is <= 0.7 and Archer prefab is assigned
        else if (randomValue <= 0.7f && Archer != null)
        {
            Instantiate(Archer, transform.position, transform.rotation);
        }
        // Spawn a Mage if no other condition is met and Mage prefab is assigned
        else if (Mage != null)
        {
            Instantiate(Mage, transform.position, transform.rotation);
        }
    }
}