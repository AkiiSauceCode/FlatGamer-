using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 1;
    public float cooldown = 2f;
    public float range = 0.5f;
    public float knockbackForce = 5f;
    public Transform attackPoint;
    public LayerMask playerLayer;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    public bool chase = false;

    [Header("Spawn Tongue Settings")]
    public GameObject tounge;
    public Transform toungeTransform;

    [Header("Hover Settings")]
    [SerializeField] private float hoverHeight = 2f;
    [SerializeField] private float hoverAmplitude = 0.3f;
    [SerializeField] private float hoverFrequency = 2f;
    [SerializeField] private float hoverXOffset = 1.5f;

    [SerializeField] private Transform startingPoint;

    private GameObject player;
    public Animator anim;
    private float hoverTimer;

    private float timer;
    private bool isAttacking;

    // Tongue spawn offset (RIGHT-facing)
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Adjust this if needed
        offset = new Vector3(0.40514f, -0.194959f, 0f);
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (player == null) return;

        // Stop movement while attacking
        if (isAttacking)
            return;

        if (CheckForPlayerWithinAttackRange() && timer <= 0)
        {
            chase = false;
            attack();
            Flip(player.transform.position.x);
            return;
        }

        if (chase)
        {
            Chase();
            Flip(player.transform.position.x);
        }
        else
        {
            ReturnToStartPoint();
        }
    }

    private void Chase()
    {
        hoverTimer += Time.deltaTime;

        float hoverOffsetY = Mathf.Sin(hoverTimer * hoverFrequency) * hoverAmplitude;
        float side = transform.position.x > player.transform.position.x ? 1f : -1f;

        Vector2 targetPosition = new Vector2(
            player.transform.position.x + hoverXOffset * side,
            player.transform.position.y + hoverHeight + hoverOffsetY
        );

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }

    public bool CheckForPlayerWithinAttackRange()
    {
        return Physics2D.OverlapCircle(
            attackPoint.position,
            range,
            playerLayer
        );
    }

    public void attack()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);
        timer = cooldown;
    }

    // Animation Event
    public void finishattacking()
    {
        anim.SetBool("isAttacking", false);
        isAttacking = false;
        chase = true;
    }

    // Animation Event
    void spawntounge()
    {
        // Mirror offset when flipped
        float direction = transform.rotation.y == 0 ? 1f : -1f;

        Vector3 spawnPos = transform.position + new Vector3(
            offset.x * direction,
            offset.y,
            offset.z
        );

        Instantiate(tounge, spawnPos, Quaternion.identity);
    }

    private void ReturnToStartPoint()
    {
        hoverTimer += Time.deltaTime;

        float hoverOffsetY = Mathf.Sin(hoverTimer * hoverFrequency) * hoverAmplitude;

        Vector2 targetPosition = new Vector2(
            startingPoint.position.x,
            startingPoint.position.y + hoverOffsetY
        );

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        Flip(startingPoint.position.x);
    }

    private void Flip(float targetX)
    {
        if (transform.position.x > targetX)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Debug range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
