using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 1;
    public float cooldown = 2;
    public float range = 0.5f;
    public float knockbackForce = 5f;
    public Transform attackPoint;
    public LayerMask playerLayer; 

    

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] public bool chase = false;

    [Header("Hover Settings")]
    [SerializeField] private float hoverHeight = 2f;
    [SerializeField] private float hoverAmplitude = 0.3f;
    [SerializeField] private float hoverFrequency = 2f;
    [SerializeField] private float hoverXOffset = 1.5f;

    [SerializeField] private Transform startingPoint;

    private GameObject player;
    public Animator anim;
    private float hoverTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private float timer;
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (player == null) return;

        if (CheckForPlayerWithinAttackRange())
        {
            Debug.Log("Attack Player");
            attack();
            Flip(player.transform.position.x);
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

        // Decide which side of the player to hover
        float side = transform.position.x > player.transform.position.x ? 1f : -1f;

        Vector2 targetPosition = new Vector2(
            player.transform.position.x + (hoverXOffset * side),
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
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, range, playerLayer);
        if (hitPlayers.Length > 0) 
        {
            Debug.Log("Payer in range");
            return true;
        }
        else
        return false;
    }

    public void attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, playerLayer);
        if (hitEnemies.Length > 0) 
        {
            // Deal damage to the player
        }

        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }
    public void finishattacking() 
    {
        anim.SetBool("isAttacking", false);
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
}
