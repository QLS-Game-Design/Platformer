using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage;
    public bool isBoss;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    Vector3 m_EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (isBoss){
            rb.velocity = -transform.up * speed;
        } else{
            rb.velocity = transform.right * speed;
        }
    }
    void FixedUpdate(){
        if (isBoss){
            transform.Rotate(0, 0, 10f);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "EnemyBullet")
        {
            Debug.Log(col.gameObject.tag);
            Instantiate(impactEffect, transform.position, transform.rotation);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>().animator.SetBool("IsHurt", false);
            Destroy(this.gameObject);
        }
            
    }
}