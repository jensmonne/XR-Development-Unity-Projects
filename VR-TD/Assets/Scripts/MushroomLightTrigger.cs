using UnityEngine;

public class MushroomLightTrigger : MonoBehaviour
{
    // Reference to the Light component to be toggled
    [SerializeField] private Light mushroomLight;

    // This method is called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Enable the mushroom light
            mushroomLight.enabled = true;
        }
    }

    // This method is called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger zone has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Disable the mushroom light
            mushroomLight.enabled = false;
        }
    }
}