using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 10f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    private Health baseHealth;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            baseHealth = collision.gameObject.GetComponent<Health>();
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            baseHealth = null; 
        }
    }
    
    private void Update()
    {
        if (baseHealth != null && Time.time >= lastAttackTime + attackCooldown)
        {
            baseHealth.TakeDamage(damageAmount);
            lastAttackTime = Time.time;
            Debug.Log(this.name + "attacked the base!");
        }
    }
}