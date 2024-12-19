using UnityEngine;

public class BaseState : MonoBehaviour
{
    // Reference to the GameManager to handle game-over logic
    [SerializeField] private GameManager gameManager;

    // Reference to the Health component to track the base's health
    [SerializeField] private Health health;

    private void Update() {
        // Check if the Health component is assigned
        if (health != null)
        {
            // Check if the GameManager is assigned
            if (gameManager != null)
            {
                // If the current health is less than or equal to 0, trigger game over
                if (health.currentHealth <= 0)
                {
                    // Call the GameOver method on the GameManager
                    gameManager.GameOver();

                    // Destroy the current game object (this base)
                    Destroy(gameObject);
                }
            }
        }
    }
}