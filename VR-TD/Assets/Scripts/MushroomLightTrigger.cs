using UnityEngine;

public class MushroomLightTrigger : MonoBehaviour
{
    [SerializeField] private Light mushroomLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mushroomLight.enabled = true;
            Debug.Log("Licht aan");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mushroomLight.enabled = false;
            Debug.Log("Licht uit");
        }
    }
}