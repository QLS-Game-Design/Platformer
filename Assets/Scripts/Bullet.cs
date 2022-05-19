using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 1f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    public AudioSource bulletHitAudio;

    // Start is called before the first frame update
    void Start()
    {
        bulletHitAudio = GameObject.Find("BulletHitAudio").GetComponent<AudioSource>();
        rb.velocity = transform.right * speed;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Bullet")
        {
            bulletHitAudio.Play();
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
            
    }
}