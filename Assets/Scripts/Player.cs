using System;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private GameObject JumpEffect;

    private bool isGrounded;
    private bool isWallSliding;
    private bool isFacingright;
    private bool isJumping;
    private Rigidbody2D rb;
    private bool isMidAirJumpLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }
    void FixedUpdate()
    {
        Move();

        if (isJumping && isGrounded && !isWallSliding)
        {
            
            GroundJump();
        }

        else if (isJumping && isWallSliding)
        {
            WallJump();
            isJumping = false;
            isGrounded = false;
        }
        else if (isJumping && isMidAirJumpLeft)
        {
            MidAirJump();
        }
    }

    private void WallJump()
    {
        Debug.Log("WallJump");
        rb.AddForce(transform.up * ((float)jumpForce / 0.5f), ForceMode2D.Impulse);
        if (isFacingright)
        {
            rb.AddForce(transform.right * ((float)jumpForce / 0.5f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(transform.right * ((float)jumpForce / 0.5f), ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += movement * Time.deltaTime * speed;
        if (Input.GetAxis("Horizontal") > 0 && !isFacingright)
        {
            isFacingright = true;
            Flip();
        }
        if (Input.GetAxis("Horizontal") < 0 && isFacingright)
        {
            isFacingright = false;
            Flip();
        }

    }

    private void GroundJump()
    {

        Debug.Log("JUMP");
        rb.AddForce(transform.up *jumpForce, ForceMode2D.Impulse);
        if (isGrounded)
        {
            isJumping = false;
        }

    }

    private void MidAirJump()
    {
        isMidAirJumpLeft = false;
        Debug.Log("MidAirJump");
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            
            isGrounded = true;
            isMidAirJumpLeft = true;
            isWallSliding = false;
        }
        else if (other.gameObject.tag == "wall")
        {
            isWallSliding = true;
            rb.gravityScale = 0.5f;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
        else if (other.gameObject.tag == "wall")
        {
            isWallSliding = false;
            rb.gravityScale = 1f;
        }
    }
    private void Flip()
    {
       
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
