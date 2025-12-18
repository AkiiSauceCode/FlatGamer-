using UnityEngine;

public class ChaseControlGround : MonoBehaviour
{
    public EnemyPatrol groundEnemy;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            groundEnemy.isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            groundEnemy.isChasing = false;
        }
    }
}
