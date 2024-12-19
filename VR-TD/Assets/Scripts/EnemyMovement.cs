using UnityEngine;
using System.Linq;

public class EnemyPathFollower : MonoBehaviour
{
    // Array of waypoints for the enemy to follow
    public Transform[] waypoints;

    // Movement speed of the enemy
    public float speed = 3f;

    // Distance threshold to determine if a waypoint is reached
    public float waypointTolerance = 1f;

    // Reference to the base object where the enemy should move after completing the path
    public Transform baseObject;

    // Index of the current waypoint in the path
    private int currentWaypointIndex;

    // Flag to check if the enemy has reached the base
    private bool isAtBase;

    private void Start()
    {
        // Initialize waypoints if not set in the editor
        if (waypoints.Length == 0)
        {
            // Find all game objects tagged as "Waypoint"
            GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");

            // Convert game objects to transforms and store in waypoints array
            waypoints = new Transform[waypointObjects.Length];
            for (int i = 0; i < waypointObjects.Length; i++)
            {
                waypoints[i] = waypointObjects[i].transform;
            }

            // Order waypoints numerically based on their names
            waypoints = waypoints.OrderBy(wp => GetWaypointNumber(wp.name)).ToArray();
        }

        // Find the base object if not assigned in the editor
        if (baseObject == null)
        {
            GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
            if (baseObj != null)
            {
                baseObject = baseObj.transform;
            }
        }
    }

    private void Update()
    {
        // Handles movement along the path and towards the base
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        // Exit if there are no waypoints
        if (waypoints.Length == 0) return;

        if (!isAtBase)
        {
            // Get the current target waypoint
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            // Adjust target position to match the enemy's Y position
            Vector3 targetPosition = new Vector3(targetWaypoint.position.x, transform.position.y, targetWaypoint.position.z);

            // Calculate the distance to the target waypoint
            float distance = Vector3.Distance(transform.position, targetPosition);

            // Move towards the target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Rotate the enemy to face the target waypoint
            RotateTowards(targetPosition);

            // Check if the waypoint is reached
            if (distance < waypointTolerance)
            {
                currentWaypointIndex++;

                // If all waypoints are completed, switch to moving towards the base
                if (currentWaypointIndex >= waypoints.Length)
                {
                    isAtBase = true;
                }
            }
        }
        else
        {
            // Move towards the base
            if (baseObject != null)
            {
                Vector3 targetBasePosition = new Vector3(baseObject.position.x, transform.position.y, baseObject.position.z);

                // Move towards the base
                transform.position = Vector3.MoveTowards(transform.position, targetBasePosition, speed * Time.deltaTime);

                // Rotate the enemy to face the base
                RotateTowards(targetBasePosition);

                // Reset the flag if the base is reached
                if (Vector3.Distance(transform.position, targetBasePosition) < waypointTolerance)
                {
                    isAtBase = false;
                }
            }
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            // Calculate the target rotation to face the direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Adjust the rotation to face backward if needed
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y - 180f, targetRotation.eulerAngles.z);

            // Smoothly interpolate the enemy's rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    // Extracts numeric value from a waypoint's name to order waypoints
    private static int GetWaypointNumber(string waypointName)
    {
        // Extract the numeric part of the waypoint name
        var numberString = new string(waypointName.Where(char.IsDigit).ToArray());
        int number;

        // Try parsing the number, default to 0 if parsing fails
        if (int.TryParse(numberString, out number))
        {
            return number;
        }

        return 0;
    }
}
