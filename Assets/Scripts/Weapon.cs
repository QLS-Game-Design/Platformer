using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float Damage;
    public GameObject player;
    Vector2 mousePos;
    public Camera mainCam;
    public Vector2 camera;
    // public Transform firePoint = this;
    public GameObject bulletPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Shoot(int k){
        GameObject clone = Instantiate(bulletPrefab,transform.position,transform.rotation);
        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), this.GetComponentInParent<Collider2D>());
        clone.GetComponent<Bullet>().speed *= k;
    }
    void Update()
    {
        mousePos = Input.mousePosition;
        camera = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(mousePos.x);
        float px = player.transform.position.x;
        float py = player.transform.position.y;
        float mx = camera.x-px;
        float my = camera.y-py;
        // Debug.Log("x:"+mx);
        // Debug.Log("y:"+my);
        if (mx < 0){
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x-0.1f, transform.position.y);
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x+0.1f, transform.position.y);
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetButtonDown("Fire1")){
            if (mx < 0)
                Shoot(-1);
            else
                Shoot(1);
        }
        float theta = Mathf.Atan(my/mx)*(180/Mathf.PI);
        this.transform.eulerAngles = new Vector3(
            this.transform.eulerAngles.x,
            this.transform.eulerAngles.y,
            theta
        );
    }
}