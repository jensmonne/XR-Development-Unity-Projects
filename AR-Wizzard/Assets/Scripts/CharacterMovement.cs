using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float rotationSpeed = 5f;

    public Camera playerCamera;
    
    [SerializeField] private GameObject character;

    private void Start()
    {
        // Automatically find the main camera if not set
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (character == null)
        {
            character = transform.Find("TB_Soldier_Mage")?.gameObject;
        }
    }

    public void MoveUp()
    {
        MoveInDirection(playerCamera.transform.forward);
    }

    public void MoveDown()
    {
        MoveInDirection(-playerCamera.transform.forward);
    }

    public void MoveLeft()
    {
        MoveInDirection(-playerCamera.transform.right);
    }

    public void MoveRight()
    {
        MoveInDirection(playerCamera.transform.right);
    }

    private void Update()
    {
        if (character == null)
        {
            Destroy(gameObject);
        }
    }

    private void MoveInDirection(Vector3 direction)
    {
        // Keep the movement on the horizontal plane (ignore vertical changes)
        direction.y = 0;

        // Calculate the new position based on the speed and deltaTime
        Vector3 move = direction.normalized * (speed * Time.deltaTime);
        transform.position += move;

        // Rotate towards the direction of movement if necessary
        if (direction != Vector3.zero)
        {
            RotateTowards(direction);
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        // Calculate the target angle in degrees
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180f;

        // Smoothly rotate towards the target angle
        float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
    }
}