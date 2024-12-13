using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerBaseTransform;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerBase = GameObject.FindGameObjectWithTag("Base");
        if (playerBase != null)
        {
            playerBaseTransform = playerBase.transform;
        }
    }

    private void Update()
    {
        if (playerBaseTransform != null)
        {
            agent.SetDestination(playerBaseTransform.position);
        }

        FaceMovementDirection();
    }

    private void FaceMovementDirection()
    {
        Vector3 velocity = agent.velocity;
        if (velocity.sqrMagnitude > 0.01f) // Check if the agent is moving
        {
            Quaternion targetRotation = Quaternion.LookRotation(-velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}