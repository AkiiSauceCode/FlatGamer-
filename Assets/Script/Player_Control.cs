using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http.Headers;
using NUnit.Framework;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
   public float speed = 8f;
   public float jumpForce = 16f;

   private float horizontal;
    private float vertical;
   
   private bool isFacingRight = true;

    public Animator animator;

    public Attack player_combat;

   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Transform groundCheck;
   [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce); 
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocityY > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y * 0.5f);
        }
        Flip();

        if(Input.GetButton("Jump")) 
        {
            animator.SetBool("isJump", true);
        }
        else 
        {
            animator.SetBool("isJump", false);
        }

        if (horizontal != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("Attack")) 
        {
            player_combat.attack();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
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


}