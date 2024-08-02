using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    private float move;
    public Rigidbody2D rb;
    bool grounded;
    private Animator anim;
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;
    private bool isFacingRight = true;

    private int jumpCount = 0;
    public int maxJumps = 2;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        move = Input.GetAxisRaw("Horizontal");

        if (move != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(new Vector2(rb.velocity.x, jump), ForceMode2D.Impulse);
            jumpCount++;
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        anim.SetBool("IsJumping", !grounded);

        if (isFacingRight && move < 0)
        {
            Flip();
        }
        else if (!isFacingRight && move > 0)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                grounded = true;
                jumpCount = 0; 
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        float dashDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
