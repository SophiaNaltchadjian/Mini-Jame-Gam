using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb;
    public int jumpForce;
    private bool isGrounded;
    private bool isFacingright;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0,0);
        transform.position += movement * Time.deltaTime * speed;
        if (Input.GetAxis("Horizontal") > 0 && isFacingright == false)
        {
            isFacingright = true;
            Flip();
        }
        if (Input.GetAxis("Horizontal") < 0 && isFacingright == true)
        {
            isFacingright = false;
            Flip();
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded==true)
        {
            rb.AddForce(transform.up *jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
    private void Flip()
    {
       
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
