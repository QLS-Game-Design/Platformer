using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject CoinsUI;

    public float speed = 5f;
    public float jumpForce = 6f;
    private float horizontalMove = 0f;
    private bool flipped = false;
    private bool onGround = true;
    // for advanced jumping
    private float timeFromLastJump = 0f;
    private bool jumpQueued = false;

    public float coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CoinsUI.GetComponent<Text>().text = "Coins: " + coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (horizontalMove < 0 && !flipped)
        {
            flipped = true;
        } else if (horizontalMove > 0 && flipped)
        {
            flipped = false;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));


        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        // // basic jumping
        // if (Input.GetButtonDown("Jump") && onGround)
        // {
        //     rb.velocity = Vector2.up * jumpForce;
        // }

        // advanced jumping with time adjustment (allow player to jump just before landing)
        if (Input.GetButtonDown("Jump"))
        {
           timeFromLastJump = 0;
           jumpQueued = true;
        }
        else
        {
           timeFromLastJump += Time.deltaTime;
        }
        if (onGround && timeFromLastJump < 0.2 && jumpQueued)
        {
           rb.velocity = Vector2.up * jumpForce;
           jumpQueued = false;
        }

        // spiderman jumping
        //if (Input.GetKey(KeyCode.W))
        //{
        //    rb.AddForce(new Vector3(0f, 0.5f, 0f), ForceMode2D.Impulse);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    rb.AddForce(new Vector3(0f, -0.5f, 0f), ForceMode2D.Impulse);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Map")
        {
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Map")
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.tag == "Coin")
        {
            coins++;
            Destroy(collision.GetComponent<Collider2D>().gameObject);
            Debug.Log(coins);
            CoinsUI.GetComponent<Text>().text = "Coins: " + coins.ToString();
        }
    }
}
