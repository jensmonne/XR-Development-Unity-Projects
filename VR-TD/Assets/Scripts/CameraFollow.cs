using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private void Start()
    {
        if (camera == null)
        {
            camera = GameObject.Find("Main Camera");
        }
    }

    private void Update()
    {
        Vector3 newPosition = camera.transform.position;
        newPosition.y -= 0.3f;
        transform.position = newPosition;
    }
}