using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifespan = 10000f;
    public Score Score;

    private void Start()
    {
        if (Score == null)
        {
            Score = FindObjectOfType<Score>();
        }
        
        Destroy(gameObject, lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Score.score += 10;
            Destroy(gameObject);
        }
    }
}