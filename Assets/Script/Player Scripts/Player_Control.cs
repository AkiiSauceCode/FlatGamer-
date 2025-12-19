using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
   public float speed = 8f;
   public float jumpForce = 16f;
   private float horizontal;
    private float vertical;
   
   private bool isFacingRight = true;

    public Animator animator;

    public Attack attackScript;

   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Transform groundCheck;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private PhysicsMaterial2D noFriction;


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce); 
        } 
        
        if (IsGrounded())
        {
            rb.sharedMaterial = null;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
            
        
        
        
        if (Input.GetButtonUp("Jump") && rb.linearVelocityY > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y * 0.5f);
        }
        Flip();

        if(Input.GetButton("Jump")) 
        { animator.SetBool("isJump", true);}
        else 
        {animator.SetBool("isJump", false);}
        if (horizontal != 0)
        {animator.SetBool("isWalking", true);}
        else
        { animator.SetBool("isWalking", false);}

        if (Input.GetButtonDown("Attack")) 
        {attackScript.attack();}
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collect"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("EndScene");
        }
    }


}