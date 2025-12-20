using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float playerDetectRange = 5f;
    public Transform detectionPoint;
    public LayerMask playerLayer;


    private float attackCooldownTimer;
    private int facingDirection = -1;
    private EnemyState enemyState;


    
    private Rigidbody2D rb;
    public Transform player;
    private Animator anim;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();
            Chase();
            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }
            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }      
    }

    void Pattrol()
    {
        rb.linearVelocity = new Vector2(facingDirection * speed, rb.linearVelocity.y);
    }

    void Chase ()
    {
        if (player.position.x > transform.position.x && facingDirection == -1 ||
                player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
            
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
           
    }


    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(detectionPoint.position, new Vector2(playerDetectRange, 2.5f), 0, playerLayer);

        if (hits.Length > 0)
        {
            //if the player is within attack range AND cooldown is ready
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && attackCooldownTimer <= 0 )
            {
            attackCooldownTimer = attackCooldown;
            ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
            ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }

    }

    public void ChangeState(EnemyState newState)
    {
        //Exit current animation
        if (enemyState == EnemyState.Idle)
        anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
        anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
        anim.SetBool("isAttacking", false);

        //Upadate our current state
        enemyState = newState;

        //Update the new animation
        if (enemyState == EnemyState.Idle)
        anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
        anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
        anim.SetBool("isAttacking", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(detectionPoint.position, new Vector2(playerDetectRange, 2.5f) );
    }
    public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback
}
}

