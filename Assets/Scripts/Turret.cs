using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Rigidbody2D rb;

    private LayerMask mapLayer;
    public float groundDetectionDistance = 0.3f;
    public float wallDetectionDistance = 0.05f;
    public float maxHealth = 3;
    public float health;
    private float timeFromLastShot = 0f;
    public float shotWaitTime = 0.5f;
    public GameObject bulletPrefab;
    public GameObject muzzle;
    public float speed = 1.5f;
    public bool flipped = false; // false is right, true is left (to change default direction can just change this field in inspector)


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapLayer = LayerMask.GetMask("Map");
        health = maxHealth;
    //     Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
    }

    void FixedUpdate()
    {
        // bool groundDetected = Physics2D.Raycast(detector.position, Vector2.down, groundDetectionDistance, mapLayer);
        // bool wallDetected = Physics2D.Raycast(detector.position, transform.right, wallDetectionDistance, mapLayer);
        
        // if (!groundDetected || wallDetected)
        // {
        //     flipped = !flipped;
        // }

        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        Shoot(flipped);
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
        GameObject.Destroy(gameObject);
    }
    
    private void Shoot(bool flipped){
        if (timeFromLastShot > shotWaitTime){
            GameObject clone = Instantiate(bulletPrefab,muzzle.transform.position, muzzle.transform.rotation);
            Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            // if (flipped){
            //     clone.GetComponent<EnemyBullet>().speed *= -1;
            // }
            timeFromLastShot = 0;
        } else{
            timeFromLastShot += Time.deltaTime;
        }
    }
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
    }
}
