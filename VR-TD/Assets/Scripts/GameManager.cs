using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Tracks whether the game is currently active
    private bool gameActive = true;

    // Tracks the player's score
    private int score = 0;

    // Timer to increment score over time
    private float scoreTimer = 0f;

    // UI element to display the score
    [SerializeField] private TextMeshProUGUI scoreText;

    // UI panel to display when the game is over
    [SerializeField] private GameObject gameOverUI;

    private void Start()
    {
        // Initialize the score display
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        // If the game is active, update the score over time
        if (gameActive)
        {
            // Increment the timer by the time elapsed since the last frame
            scoreTimer += Time.deltaTime;

            // If one second has passed, increment the score
            if (scoreTimer >= 1f)
            {
                score++;
                scoreText.text = "Score: " + score;
                scoreTimer = 0f; // Reset the timer
            }
        }
    }

    // Called when the game is over
    public void GameOver()
    {
        // Stop the game
        gameActive = false;

        // Destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Destroy all spawners
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }

        // Display the Game Over UI
        gameOverUI.SetActive(true);
    }

    // Restart the current scene to reset the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}