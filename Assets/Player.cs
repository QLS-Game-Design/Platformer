using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 0.5f;
    private bool onGround = true;
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				onGround = true;
			}
		}

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        // if(Input.GetKey(KeyCode.D))
        // {
        //     //transform.Translate(Vector2.right * speed * Time.deltaTime);
            
        // }
        // if(Input.GetKey(KeyCode.A)){
        //     //transform.Translate(Vector2.left * speed * Time.deltaTime);
        //     rb.velocity = new Vector2(-speed, rb.velocity.y);
        // }
        if(Input.GetKey(KeyCode.W) && onGround){
            Debug.Log("Jump");
            // rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode2D.Impulse);
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode2D.Impulse);
            onGround = false;
        }
    }
}
