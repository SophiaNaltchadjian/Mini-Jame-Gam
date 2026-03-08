using System;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject JumpEffect;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float maxWallFallSpeed;
    [SerializeField] private float acceleration;
    [Header("Gravity Multipliers")]
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float riseGravityMultiplier;
    [Header("State Flags")]

    private bool isGrounded;
    private bool isWallSliding;
    private bool isFacingright;
    private bool isJumping;
    private Rigidbody2D rb;
    private bool isMidAirJumpLeft;
    private float horizontal;
    private bool isWhistleCollected;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        //Why did you make this GroundJump? 
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }
    void FixedUpdate()
    {
        ApplyCustomGravity(rb);
        Move();
        if (isJumping && isGrounded)
        {
            GroundJump();
        }

        else if (isJumping && isWallSliding)
        {
            WallJump();

        }
        else if (isJumping && isMidAirJumpLeft)
        {
            MidAirJump();
        }
        
        isJumping = false;
    }

    private void ApplyCustomGravity(Rigidbody2D rigidbody2D)
    {
        float gravityMultiplier = 1f;

        if (rigidbody2D.linearVelocity.y > 0f)
        {
            gravityMultiplier = riseGravityMultiplier;
        }

        if (rigidbody2D.linearVelocity.y < 0f)
        {
            gravityMultiplier = fallGravityMultiplier;
        }

        float newVerticalVelocity = rigidbody2D.linearVelocity.y - gravity * gravityMultiplier * Time.fixedDeltaTime;

        if (isWallSliding)
        {
            newVerticalVelocity = Mathf.Max(newVerticalVelocity, -maxWallFallSpeed);

        }
        else
        {
            newVerticalVelocity = Mathf.Max(newVerticalVelocity, -maxFallSpeed);

        }

        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, newVerticalVelocity);
    }
    private void WallJump()
    {
        Debug.Log("WallJump");
        float wallKickForce = isFacingright ? -wallJumpForce : wallJumpForce;
        rb.linearVelocity = new Vector2(wallKickForce, jumpForce);
    }

    private void Move()
    {
        
        float targetSpeed = horizontal * maxSpeed;
        float newSpeed = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newSpeed, rb.linearVelocity.y);
        if (horizontal > 0 && !isFacingright)
        {
            isFacingright = true;
            Flip();
        }
        if (horizontal < 0 && isFacingright)
        {

            isFacingright = false;
            Flip();
        }
    }

    private void GroundJump()
    {

        Debug.Log("JUMP");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

      
    }

    private void MidAirJump()
    {
        isMidAirJumpLeft = false;
        Debug.Log("MidAirJump");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGrounded = true;
            isMidAirJumpLeft = true;
        }
        if (other.gameObject.tag == "wall")
        {
            isWallSliding = true;
        }
        if (other.gameObject.GetComponent<Whistle>())
        {
            Destroy(other.gameObject);
            isWhistleCollected = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
        if (other.gameObject.tag == "wall")
        {
            isWallSliding = false;
        }
    }
    private void Flip()
    {
       
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
