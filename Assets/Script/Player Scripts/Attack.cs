using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anim;

    [Header("Attack Settings")]
    public int damage = 1;
    public float cooldown = 2;
    public float range = 0.5f;
    public float knockbackForce = 5f;
    public Transform attackPoint;
    public LayerMask enemyLayer; 

     private GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    public void attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        if (hitEnemies.Length > 0) 
        {
            foreach (Collider2D enemy in hitEnemies) 
            {
                enemy.GetComponent<EnemyHealth>().ChangeHealth(-damage);
            }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
