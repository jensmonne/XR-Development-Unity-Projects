using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Maximum health value for this object
    public float maxHealth = 100f;

    // Current health of the object
    public float currentHealth;

    // UI Image component to represent the health bar
    public Image healthbarFill;

    private void Start()
    {
        // Initialize current health to the maximum health at the start
        currentHealth = maxHealth;

        // Update the health bar UI to reflect the initial health
        UpdateHealthBar();
    }

    // Updates the health bar UI based on the current health value
    private void UpdateHealthBar()
    {
        if (healthbarFill != null)
        {
            // Adjust the fill amount of the health bar to match the health ratio
            healthbarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    // Reduces the current health by the specified damage amount
    public void TakeDamage(float amount)
    {
        // Subtract the damage from the current health
        currentHealth -= amount;

        // Clamp the health value between 0 and the maximum health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar UI after taking damage
        UpdateHealthBar();

        // Check if health has reached zero and destroy the object if so
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}