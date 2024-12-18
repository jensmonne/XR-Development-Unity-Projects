using UnityEngine;
using System.Linq;

public class EnemyPathFollower : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints the enemy will follow
    public float speed = 3f;       // Speed of the enemy
    public float waypointTolerance = 1f;  // Tolerance radius for reaching a waypoint (1 unit)
    public Transform baseObject;  // Reference to the Base object the enemy will go to after the last waypoint

    private int currentWaypointIndex;
    private bool isAtBase; // Flag to check if the enemy has reached the base

    private void Start()
    {
        if (waypoints.Length == 0)
        {
            GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");

            waypoints = new Transform[waypointObjects.Length];

            for (int i = 0; i < waypointObjects.Length; i++)
            {
                waypoints[i] = waypointObjects[i].transform;
            }

            waypoints = waypoints.OrderBy(wp => GetWaypointNumber(wp.name)).ToArray();
        }

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
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (waypoints.Length == 0) return;

        if (!isAtBase)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Vector3 targetPosition = new Vector3(targetWaypoint.position.x, transform.position.y, targetWaypoint.position.z);
            float distance = Vector3.Distance(transform.position, targetPosition);

            // Move towards the target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Rotate to face the movement direction
            RotateTowards(targetPosition);

            if (distance < waypointTolerance)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypoints.Length)
                {
                    isAtBase = true;
                }
            }
        }
        else
        {
            if (baseObject != null)
            {
                Vector3 targetBasePosition = new Vector3(baseObject.position.x, transform.position.y, baseObject.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetBasePosition, speed * Time.deltaTime);

                // Rotate to face the movement direction
                RotateTowards(targetBasePosition);

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

        // Ignore Y-axis rotation to keep the object upright
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Adjust the target rotation by 180 degrees around the Y-axis
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y - 180f, targetRotation.eulerAngles.z);

            // Smoothly interpolate to the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
    
    private static int GetWaypointNumber(string waypointName)
    {
        var numberString = new string(waypointName.Where(char.IsDigit).ToArray());
        int number;

        if (int.TryParse(numberString, out number))
        {
            return number;
        }

        return 0;
    }
}