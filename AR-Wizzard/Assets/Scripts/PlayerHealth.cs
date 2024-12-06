using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Score Score;

    private void Start()
    {
        Score = FindObjectOfType<Score>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Score.Die();
        }
    }
}