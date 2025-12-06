using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    public FlyingEnemyMovement flyingEnemy;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            flyingEnemy.chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            flyingEnemy.chase = false;
        }
    }
}
