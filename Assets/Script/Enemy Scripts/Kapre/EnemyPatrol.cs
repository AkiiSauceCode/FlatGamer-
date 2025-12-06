using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform currentpoint;
    public float speed;

    public float waitduration;
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
        Vector2 point = currentpoint.position - transform.position;
        if (currentpoint == point2.transform) 
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else 
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == point2.transform) 
        {
            flip();
            StartCoroutine(waitnextpoint());
            currentpoint = point1.transform;
        }
        if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == point1.transform)
        {
            flip();
            StartCoroutine(waitnextpoint());
            currentpoint = point2.transform;
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
}
