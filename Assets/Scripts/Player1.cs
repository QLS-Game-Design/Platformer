using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public GameObject CoinsUI;
    public GameObject healthBar;
    public Transform detector;
    public LayerMask mapLayer;

    public float speed = 5f;
    public float maxHealth = 10f;
    public float health;
    public float jumpForce = 6f;
    public float horizontalMove = 0f;
    public bool onGround = true;
    // for advanced jumping
    private float timeFromLastJump = 0f;
    private bool jumpQueued = false;
    public float coins = 0;


    // for weapon attacks
    private float deathDuration = 3f;
    private float punchDistance = 0.2f;
    private float punchDuration = 3f;
    private bool weaponFlip = false;


    private float gravityWhileClimb = 0.2f;
    private float wallDetectDistance = 0.5f;

    public AudioSource playerHurtAudio;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = StaticTracker.maxHealth;
        coins = StaticTracker.coins;
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CoinsUI.GetComponent<Text>().text = coins.ToString();
        playerHurtAudio = GameObject.Find("PlayerHurtAudio").GetComponent<AudioSource>();
        animator.SetBool("JumpUp", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            return;
        }
        // horizontal movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        weaponFlip = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>().isFlipped;
        if (horizontalMove < 0 && weaponFlip)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = true;
        } else if (horizontalMove > 0 && !weaponFlip)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = false;
        }
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
        //if (Input.GetButtonDown("Fire2")){
        //    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>().NextPos();
        //    Punch();
        //}



        // Slower gravity while climbing
        if (Physics2D.Raycast(detector.position, Vector2.right, wallDetectDistance, mapLayer) == true || Physics2D.Raycast(detector.position, Vector2.left, 2*wallDetectDistance, mapLayer) == true){
            if (GetComponent<Rigidbody2D>().velocity.y < 0){
                GetComponent<Rigidbody2D>().gravityScale = gravityWhileClimb;
            } else {
                GetComponent<Rigidbody2D>().gravityScale = 1.3f;
            }
        } else {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        
    }
    IEnumerator Punch(){
        animator.SetBool("Punch", true);
        yield return new WaitForSeconds(punchDuration);
        animator.SetBool("Punch", false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Map")
        {
            onGround = true;
            animator.SetBool("JumpUp", false);
        }   else if (collision.collider.gameObject.tag == "Enemy")
        {
            health-=1;
            playerHurtAudio.Play();
            animator.SetBool("IsHurt", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+5);
        } else if (collision.collider.gameObject.tag == "EnemyBullet")
        {
            health-=collision.collider.gameObject.GetComponent<EnemyBullet>().damage;
            playerHurtAudio.Play();
            animator.SetBool("IsHurt", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+5);
        }
        healthBar.GetComponent<Slider>().value = health / maxHealth;
        if (health <= 0)
        {
            animator.SetBool("IsHurt", false);
            animator.SetBool("JumpUp", false);
            StartCoroutine(Die());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (health <= 0)
        {
            return;
        }
        if (collision.collider.gameObject.tag == "Map")
        {
            onGround = false;
            animator.SetBool("JumpUp", true);
        } else if (collision.collider.gameObject.tag == "Enemy")
        {
            animator.SetBool("IsHurt", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0)
        {
            return;
        }
        if (collision.gameObject.tag == "Respawn")
        {
            health = 0;
            animator.SetBool("IsHurt", false);
            animator.SetBool("JumpUp", false);
            StartCoroutine(Die());
        }
        if (collision.GetComponent<Collider2D>().gameObject.tag == "Coin")
        {
            coins++;
            Destroy(collision.GetComponent<Collider2D>().gameObject);
            CoinsUI.GetComponent<Text>().text = coins.ToString();
        }
    }
    IEnumerator Die()
	{
        Debug.Log("died");
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(deathDuration);
        Application.LoadLevel(Application.loadedLevel);
	}
}
