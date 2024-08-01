using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PLayerMovement : MonoBehaviour
{

    public float speed;

    public float jump;

    private float Move;

    public Rigidbody2D rb;

    bool grounded;

    private Animator anim;

   // public Transform groundCheck;
    // public float groundRadius;

    private bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight=true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * Move * Time.deltaTime, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            grounded = false;
        }

        if (Move != 0)
        {
            anim.SetBool("IsRuning", true);      
        }
        else
        {
            anim.SetBool("IsRuning", false);
        }

        anim.SetBool("IsJumping", !grounded);

        if (isFacingRight && Move < 0)
        {
            Flip();
        }
        else if (!isFacingRight && Move > 0)
            {
            Flip();
            }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                grounded = true;
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
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Move * speed * Time.deltaTime, rb.velocity.y);
    }

}
