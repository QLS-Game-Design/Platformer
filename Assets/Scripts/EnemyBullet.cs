using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 1f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -transform.up * speed;
        rb.angularVelocity = 20f;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "EnemyBullet")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>().animator.SetBool("IsHurt", false);
            Destroy(this.gameObject);
            Debug.Log(col.gameObject.tag);
        }
            
    }
}