using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    Vector3 m_EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -transform.up * speed;
    }
    void FixedUpdate(){
        transform.Rotate(0, 0, 10f);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "EnemyBullet")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>().animator.SetBool("IsHurt", false);
            Destroy(this.gameObject);
        }
            
    }
}