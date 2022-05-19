using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 1f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    Vector3 m_EulerAngleVelocity;

    public AudioSource grenadeHitAudio;

    // Start is called before the first frame update
    void Start()
    {
        grenadeHitAudio = GameObject.Find("GrenadeHitAudio").GetComponent<AudioSource>();
        rb.velocity = transform.right * speed;
    }
    void FixedUpdate(){
        transform.Rotate(0, 0, 10f);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Grenade")
        {
            grenadeHitAudio.Play();
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
            
    }
}