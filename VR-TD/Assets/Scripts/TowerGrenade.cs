using UnityEngine;

public class TowerGrenade : MonoBehaviour
{
    public GameObject towerPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}