using UnityEngine;

public class PlayerBawang : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public float lifeTime = 10f;

    private Rigidbody2D rb;
    private Transform target;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindNearestEnemy();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            rb.linearVelocity = rb.linearVelocity; // keep moving forward
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // Rotate projectile to face target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
    }

    // =========================
    // Find Nearest Enemy
    // =========================
    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    // =========================
    // Collision
    // =========================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.ChangeHealth(-damage);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
