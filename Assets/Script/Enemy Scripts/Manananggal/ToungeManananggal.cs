using UnityEngine;

public class ToungeManananggal : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    

    private int dam;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
 
        Vector3 direction = player.transform.position - transform.position;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
    
        
    }

    public void Removetounge() 
    {
        Destroy(gameObject);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-dam);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }
}
