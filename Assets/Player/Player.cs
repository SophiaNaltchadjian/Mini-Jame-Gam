using System;
using System.Linq.Expressions;

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
    private bool isSliding;
    private bool isFacingRight;
    private bool isJumpingPressed;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isMidAirJumpLeft;
    private float horizontal;
    private WhistleHandler whistleHandler;
    private float whistleCooldownTimer;
    private bool isOnSlidingObject;
    private GameObject currentGround;
    public AudioSource playerAudioSource;
    public AudioClip[] playerAudioClips;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        whistleHandler = GetComponent<WhistleHandler>();
        playerAudioSource = GetComponent<AudioSource>();
    }



    void Update()
    {
       
        horizontal = isOnSlidingObject ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal");

        if (whistleCooldownTimer > 0f)
            whistleCooldownTimer -= Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.E)) && whistleCooldownTimer <= 0f && whistleHandler.CanBlow())
        {
            whistleHandler.BlowWhistle();
            whistleCooldownTimer = 3f;
        }

        if (Input.GetButtonDown("Jump"))

        {
            AudioClip audioToPlay = playerAudioClips[0];
            playerAudioSource.clip = audioToPlay;
            playerAudioSource.Play();
            isJumpingPressed = true;
        }
        if (Input.GetAxis("Horizontal") > 0 && isFacingRight==false)
        {
            FlipToRight();
            
        }
        else if(Input.GetAxis("Horizontal") < 0 && isFacingRight == true)
        {
            FlipToLeft();
        }
        animator.SetBool("IsGrounded", isGrounded); 
        animator.SetBool("IsRunning", Mathf.Abs(horizontal) > 0.01f && isGrounded);
        //animator.SetBool("IsMidAirJumping", !isMidAirJumpLeft && !isGrounded);
        animator.SetBool("IsSliding", isSliding && isGrounded);
        animator.SetBool("IsWallSliding", isWallSliding && !isGrounded);
       // animator.SetBool("IsJumping", isJumpingPressed);
    }
    void FixedUpdate()
    {
        ApplyCustomGravity(rb);
        Move();
        //『ありがと』s

        if (isJumpingPressed && isGrounded && !isWallSliding)
        {
            
            GroundJump();

            animator.SetTrigger("PlayJump");
        }

        else if (isJumpingPressed && isWallSliding)
        {
            WallJump();
            animator.SetTrigger("PlayJump");

        }
        else if (isJumpingPressed && isMidAirJumpLeft)
        {
            MidAirJump();
            animator.SetTrigger("PlayAirJump");
        }
        
        isJumpingPressed = false;
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
        float wallKickForce = wallJumpForce;
        if (isFacingRight)
        {
            wallKickForce = -wallJumpForce;
        }
        rb.linearVelocity = new Vector2(wallKickForce, jumpForce);
    }
    void FlipToLeft()
    {
        isFacingRight = false;
        Vector3 scale = transform.localScale;
        scale.x = 1;
        transform.localScale = scale;
    }
    void FlipToRight()
    {
        isFacingRight = true;
        Vector3 scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;
    }
    private void Move()
    {
        

        float newX = rb.linearVelocity.x + horizontal * acceleration * Time.fixedDeltaTime;
        newX = Mathf.Clamp(newX, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
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

    public void CheckSlidingObject()
    {
        if (currentGround != null && currentGround.TryGetComponent<Freezable>(out Freezable freezable))
        {
            isOnSlidingObject = freezable.IsFrozen;
            if(freezable.IsFrozen == true)
            {
                isSliding = true;
                isOnSlidingObject =true;
            }
            else
            {
                isSliding=false;
                isOnSlidingObject = false;
            }
            
        }
        else
        {
            isOnSlidingObject = false;
        
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {

            isGrounded = true;
            isMidAirJumpLeft = true;
            currentGround = other.gameObject;
            CheckSlidingObject();
        }
        if (other.gameObject.tag == "wall")
        {
            isWallSliding = true;
        }
        
        if (other.gameObject.TryGetComponent<Whistle>(out _))
        {
            Destroy(other.gameObject);
            whistleHandler.Collect();
        }
        if (other.gameObject.TryGetComponent<GameEnding>(out _))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGrounded = false;
            currentGround = null;
            isOnSlidingObject = false;
        }
        if (other.gameObject.tag == "wall")
        {
            isWallSliding = false;
        }
    }
}
