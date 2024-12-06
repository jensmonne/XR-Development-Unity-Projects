using System;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public GameObject playerFollow;
    public float speed = 0.5f;

    private void Start()
    {
        if (playerFollow == null)
        {
            playerFollow = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (transform.Find("TB_Soldier_Mage") == null)
        {
            Destroy(gameObject);
        }
        
        if (playerFollow == null) return;

        Vector3 direction = (playerFollow.transform.position - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 cameraDirection = (mainCamera.transform.position - transform.position).normalized;
            cameraDirection = -cameraDirection;

            Quaternion lookRotation = Quaternion.LookRotation(cameraDirection);

            lookRotation.x = 0f;
            lookRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}