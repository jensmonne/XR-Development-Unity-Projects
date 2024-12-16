using UnityEngine;

public class BaseState : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Health health;

    private void Update() {
        if (health != null)
        {
            if(gameManager != null)
            {
                if(health.currentHealth <= 0)
                {
                    gameManager.GameOver();
                    Destroy(gameObject);
                }
            } else
            {
                Debug.Log("Base has no reference to GameManager");
            }
            
        } else 
        {
            Debug.Log("Base has no reference to Health");
        }
    }
}