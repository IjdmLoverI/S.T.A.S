using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMovement : MonoBehaviour
{

    public float speed;
    public float jump;

    private float Move;

    public Rigidbody2D rb;

    public bool isJumping;

    private Animator anim;

    private bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight=true;
        isJumping = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        if (Move != 0)
        {
            anim.SetBool("IsRuning", true);      
        }
        else
        {
            anim.SetBool("IsRuning", false);
        }

        if (jump != 0)
        {
            anim.SetBool("IsJumping", true);
        }
        else 
        {
            anim.SetBool("IsJumping", false);
        }

        if (isFacingRight && Move < 0)
        {
            Flip();
        }
        else if (!isFacingRight && Move > 0)
            {
            Flip();
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }

    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        { 
            isJumping = true;
        }
    }
}
