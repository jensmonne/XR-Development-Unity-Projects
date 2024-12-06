using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    private Transform cameraTransform; // Reference to the camera's transform
    public Transform bodyTransform;
    public Transform headTransform;   // Reference to the character's body (or forward direction)
    public float maxAngle = 90f;      // Maximum angle for head tracking (180 degrees front view, so half is 90 degrees)
    public float rotSpeed = 5f; 

    private Animator animator;

    void Start(){
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the direction from the head to the camera
        Vector3 directionToCamera = (cameraTransform.position - headTransform.position).normalized;

        // Project the direction onto the character's forward plane to get a 2D-like angle
        Vector3 forwardDirection = bodyTransform.forward;
        forwardDirection.y = 0; // Ignore vertical differences for left-right check

        // Get the angle between the forward direction of the body and the direction to the camera
        float angleToCamera = Vector3.Angle(forwardDirection, directionToCamera);

        // Check if the camera is within the 180-degree field of view in front of the model
        if (angleToCamera <= maxAngle)
        {
            // Rotate the head to look at the camera, including up and down
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Smoothly rotate the head
            headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, Time.deltaTime * rotSpeed); // Adjust speed if needed
        
            animator.SetBool("MouthOpen", true);
        }
        else
        {
            // Rotate the head back to facing forward if the camera is behind the character
            Quaternion targetRotation = Quaternion.LookRotation(bodyTransform.forward);

            // Smoothly rotate the head back to forward
            headTransform.rotation = Quaternion.Slerp(headTransform.rotation, targetRotation, Time.deltaTime * rotSpeed);

            animator.SetBool("MouthOpen", false);
        
        }
    }
}
