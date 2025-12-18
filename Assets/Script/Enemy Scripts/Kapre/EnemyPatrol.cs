using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public GameObject point1;
    public GameObject point2;

    [Header("Movement")]
    public float speed = 2f;
    private Transform currentPoint;

    [Header("Ground Detection")]
    public Transform ledgeDetector;
    public LayerMask ground;
    public float raycastDistance = 1f;

    [Header("Chase Settings")]
    public float chaseDistance = 5f;
    public float waitDuration = 1f;

    [Header("Combat")]
    public int damage = 1;
    public float attackCooldown = 1f; // ✅ cooldown (1 second)

    private float attackTimer = 0f;    // ✅ cooldown timer

    private Animator anim;
    private Rigidbody2D rb;
    private Transform playerTransform;

    public bool isChasing = false;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentPoint = point2.transform;
        anim.SetBool("isRunning", true);

        FindPlayer();
    }

    void Update()
    {
        // ✅ cooldown countdown
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;

        if (!canMove || playerTransform == null)
            return;

        DetectLedge();

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    void Patrol()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        Flip(direction.x);

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            StartCoroutine(SwitchPoint());
    }

    IEnumerator SwitchPoint()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);

        yield return new WaitForSeconds(waitDuration);

        currentPoint = (currentPoint == point1.transform) ? point2.transform : point1.transform;
        anim.SetBool("isRunning", true);
        canMove = true;
    }

    void ChasePlayer()
    {
        float direction = playerTransform.position.x - transform.position.x;
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * speed, rb.linearVelocity.y);
        Flip(direction);

        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            isChasing = false;
    }

    void DetectLedge()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            ledgeDetector.position,
            Vector2.down,
            raycastDistance,
            ground
        );

        if (!hit)
        {
            rb.linearVelocity = Vector2.zero;
            isChasing = false;
            StartCoroutine(SwitchPoint());
        }
    }

    // =========================
    // ATTACK (Cooldown Added)
    // =========================
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (attackTimer > 0f) // ✅ cooldown check
            return;

        anim.SetBool("isAttacking", true);
        rb.linearVelocity = Vector2.zero;
        canMove = false;

        attackTimer = attackCooldown; // ✅ start cooldown
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isAttacking", false);
            canMove = true;
        }
    }

    void Flip(float direction)
    {
        if (direction > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else if (direction < 0)
            transform.localScale = new Vector3(-2, 2, 2);
    }

    void OnDrawGizmos()
    {
        if (point1 != null && point2 != null)
        {
            Gizmos.DrawWireSphere(point1.transform.position, 0.5f);
            Gizmos.DrawWireSphere(point2.transform.position, 0.5f);
            Gizmos.DrawLine(point1.transform.position, point2.transform.position);
        }
    }
}
