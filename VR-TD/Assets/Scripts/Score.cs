using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public int score;
    public int highScore;
    private void Start()
    {
        score = PlayerPrefs.GetInt("SavedScore", 0);
        highScore = PlayerPrefs.GetInt("SavedHighScore", 0);

        if (highScoreText == null)
        {
            GameObject scoreObject = GameObject.FindGameObjectWithTag("HighScore");
            if (scoreObject != null)
            {
                highScoreText = scoreObject.GetComponent<TMP_Text>();
            }
        }

        if (scoreText == null)
        {
            GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
            if (scoreObject != null)
            {
                scoreText = scoreObject.GetComponent<TMP_Text>();
            }
        }
    }

    private void Update()
    {
        if (scoreText != null)
        {
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                scoreText.text = "Your Score: " + score;
                if (score > highScore)
                {
                    highScore = score;
                }
            }
            else
            {
                scoreText.text = "Score: " + score;
            }
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
    
    public void RestartButton()
    {
        score = 0;
        PlayerPrefs.SetInt("SavedHighScore", highScore);
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void Die()
    {
        PlayerPrefs.SetInt("SavedScore", score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        PlayerPrefs.SetInt("SavedHighScore", highScore);
        PlayerPrefs.DeleteKey("SavedScore");
        Debug.Log("Quit");
        Application.Quit();
    }
}
