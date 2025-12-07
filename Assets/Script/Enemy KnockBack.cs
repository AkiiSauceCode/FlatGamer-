using UnityEngine;

public class Enemy_KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

        public void Knockback(Transform plaryer, float knockbackForce)
    {
        Vector2 direction = (transform.position - plaryer.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
        Debug.Log("Knockback applied");



        
    }
}
