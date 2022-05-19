using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    private LayerMask mapLayer;
    public Transform detector;
    public float groundDetectionDistance = 0.3f;
    public float wallDetectionDistance = 0.05f;
    public float maxHealth = 3;
    public float health;

    public float speed = 1.5f;
    public bool flipped = false; // false is right, true is left (to change default direction can just change this field in inspector)

    public AudioSource enemyDeathAudio;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapLayer = LayerMask.GetMask("Map");
        health = maxHealth;
        enemyDeathAudio = GameObject.Find("EnemyDeathAudio").GetComponent<AudioSource>();
        //     Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
    }

    void FixedUpdate()
    {

        bool groundDetected = Physics2D.Raycast(detector.position, Vector2.down, groundDetectionDistance, mapLayer);
        bool wallDetected = Physics2D.Raycast(detector.position, transform.right, wallDetectionDistance, mapLayer);
        
        if (!groundDetected || wallDetected)
        {
            flipped = !flipped;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        rb.velocity = new Vector2(flipped ? -speed : speed, rb.velocity.y);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 2f, 0f), ForceMode2D.Impulse);
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        enemyDeathAudio.Play();
        GameObject.Destroy(gameObject);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(detector.position, new Vector2(detector.position.x, detector.position.y - groundDetectionDistance));
    //    Gizmos.DrawLine(detector.position, new Vector2(detector.position.x + (flipped ? -wallDetectionDistance : wallDetectionDistance), detector.position.y));
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.collider.gameObject.GetComponent<Bullet>().damage);
        }
        if (collision.collider.gameObject.tag == "Grenade")
        {
            TakeDamage(collision.collider.gameObject.GetComponent<Grenade>().damage);
        }
        if (collision.collider.gameObject.tag == "Enemy")
        {
            flipped = !flipped;
        }
    }
}
