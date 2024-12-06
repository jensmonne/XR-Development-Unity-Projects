using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float moveSpeed = 0.3f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * (moveSpeed * Time.deltaTime);
        }
    }
}