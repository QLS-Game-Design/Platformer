using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 2f;
    private bool onGround = true;
    private bool jumpingCondition;
    public LayerMask groundLayerMask;
    public Transform groundCheck;
    public float groundCheckRadius;
    public float horizontalMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        if(Input.GetKey(KeyCode.W)){
            jumpingCondition = true;
        }
        if(Input.GetKey(KeyCode.S)){
            rb.AddForce(new Vector3(0f, -0.5f, 0f), ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        if (jumpingCondition && onGround){
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Map")
        {
            onGround = true;
            jumpingCondition = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Map")
        {
            onGround = false;
        }
    }
}
