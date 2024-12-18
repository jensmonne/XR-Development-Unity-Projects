using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Mage;
    [SerializeField] private GameObject Tank;
    [SerializeField] private GameObject Spearman;
    [SerializeField] private GameObject Archer;

    public float spawnInterval = 5f;
    private float lastSpawnTime;

    private void Update()
    {
        if (Time.time >= lastSpawnTime + spawnInterval)
        {
            SpawnEnemies();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemies()
    {
        // Generate a random number between 0 and 1
        float randomValue = Random.Range(0f, 1f);

        // 1/5 chance to spawn Tank, otherwise spawn Mage
        if (randomValue <= 0.2f && Tank != null)
        {
            Instantiate(Tank, transform.position, transform.rotation);
        }
        else if (randomValue <= 0.5f && Spearman != null)
        {
            Instantiate(Spearman, transform.position, transform.rotation);
        }
        else if (randomValue <= 0.7f && Archer != null)
        {
            Instantiate(Archer, transform.position, transform.rotation);
        }
        else if (Mage != null)
        {
            Instantiate(Mage, transform.position, transform.rotation);
        }
    }
}