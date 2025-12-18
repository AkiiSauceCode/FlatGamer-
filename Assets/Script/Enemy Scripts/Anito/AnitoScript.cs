using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;

public class AnitoScript : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform currentpoint;
    public float speed;
    public Transform ledgedetector;
    public LayerMask ground;
    public float raycastDistance;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public float waitduration;

    public int dam;
    public bool condition = true;
    public bool path = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentpoint = point2.transform;
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        condition = true;
        path = true;
        if (condition)
        {
            RaycastHit2D hit = Physics2D.Raycast(ledgedetector.position, Vector2.down, raycastDistance, ground);

            if (hit.collider == null)
            {
                flip();
                chaseDistance = 0;
                isChasing = false;
                path = true;
            }

            if (path)
            {
                Vector2 point = currentpoint.position - transform.position;
                if (currentpoint == point2.transform)
                {
                    transform.localScale = new Vector3(2, 2, 2);
                    rb.linearVelocity = new Vector2(speed, 0);
                }
                else
                {
                    transform.localScale = new Vector3(-2, 2, 2);
                    rb.linearVelocity = new Vector2(-speed, 0);
                }

                if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == point2.transform)
                {
                    chaseDistance = 5;
                    flip();
                    StartCoroutine(waitnextpoint());
                    currentpoint = point1.transform;
                }
                if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == point1.transform)
                {
                    chaseDistance = 5;
                    flip();
                    StartCoroutine(waitnextpoint());
                    currentpoint = point2.transform;
                }

                else if (isChasing)
                {
                    if (transform.position.x > playerTransform.position.x)
                    {
                        transform.localScale = new Vector3(-2, 2, 2);
                        transform.position += Vector3.left * speed * Time.deltaTime;
                        path = false;
                    }
                    else if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
                    {
                        isChasing = false;
                        path = true;
                    }
                    if (transform.position.x < playerTransform.position.x)
                    {
                        transform.localScale = new Vector3(2, 2, 2);
                        transform.position += Vector3.right * speed * Time.deltaTime;
                        path = false;
                    }
                    else if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
                    {
                        isChasing = false;
                        path = true;
                    }
                }
                else
                {
                    if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
                    {
                        isChasing = true;
                        path = false;
                    }
                }

            }
        }

    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(point2.transform.position, 0.5f);
        Gizmos.DrawLine(point1.transform.position, point2.transform.position);
    }

    IEnumerator waitnextpoint()
    {
        speed = 0;
        anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(waitduration);
        speed = 2;
        anim.SetBool("isRunning", true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isAttacking", true);
            condition = false;
            speed = 0;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(ledgedetector.position, Vector2.down, raycastDistance, ground);

        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isAttacking", false);
            speed = 3;
            condition = true;
            chaseDistance = 5;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-dam);
        }
    }
}

