using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnPoint;
    public float fireballSpeed = 10f;
    public int spawnDelay = 1;
    private bool canShoot = true;
    private void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.Find("XR Origin").transform;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            ShootFireball();
            StartCoroutine(HandleSpawnDelay());
        }
    }

    private void ShootFireball()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPosition = touch.position;
        //Vector3 clickPosition = Input.mousePosition;
        //Ray ray = Camera.main.ScreenPointToRay(clickPosition);
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        Vector3 targetDirection;

        if (Physics.Raycast(ray, out hit))
        {
            targetDirection = (hit.point - spawnPoint.position).normalized;
        }
        else
        {
            targetDirection = Camera.main.transform.forward;
        }

        GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = targetDirection * fireballSpeed;
    }

    private IEnumerator HandleSpawnDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(spawnDelay);
        canShoot = true;
    }
}