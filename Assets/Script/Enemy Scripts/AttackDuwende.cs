using UnityEngine;

public class AttackDuwende : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask layerA;
    public float attackradius = 1.0f;

    public void attack() 
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackradius, layerA);
        if (hit) 
        {
            Debug.Log(hit.transform.name);
        }
    }
}
