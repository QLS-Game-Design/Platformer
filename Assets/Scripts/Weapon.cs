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
    public bool isFlipped = false;
    public float weaponOffsetIdleX;
    public float weaponOffsetIdleY;
    public float weaponOffsetRunningX;
    public float weaponOffsetRunningY;
    Sprite weaponSprite;
    void Start()
    {
        weaponSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Shoot(int k){
        GameObject clone = Instantiate(bulletPrefab,GameObject.FindGameObjectWithTag("Muzzle").transform.position,transform.rotation);
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

        if (!player.GetComponent<Player1>().onGround)
        {
            GetComponent<SpriteRenderer>().sprite = null;
            return;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = weaponSprite;
        }

        float weaponOffsetX, weaponOffsetY;
        if (Mathf.Abs(player.GetComponent<Player1>().horizontalMove) > 0)
        {
            weaponOffsetX = weaponOffsetRunningX;
            weaponOffsetY = weaponOffsetRunningY;
        }
        else
        {
            weaponOffsetX = weaponOffsetIdleX;
            weaponOffsetY = weaponOffsetIdleY;
        }

        if (mx < 0){ // if the mouse is to the left of the character
            //GetComponent<SpriteRenderer>().flipX = true;
            //isFlipped = true;
            transform.position = new Vector3(px-weaponOffsetX, py+weaponOffsetY, -1);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = true;
            
            Vector3 theScale = GameObject.FindGameObjectWithTag("Player").transform.localScale;
            if (theScale.x > 0)
            {
                theScale.x *= -1;
                GameObject.FindGameObjectWithTag("Player").transform.localScale = theScale;
            }
        } else {
            //GetComponent<SpriteRenderer>().flipX = false;
            //isFlipped = false;
            transform.position = new Vector3(px+weaponOffsetX, py+weaponOffsetY, -1);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = false;

            Vector3 theScale = GameObject.FindGameObjectWithTag("Player").transform.localScale;
            if (theScale.x < 0)
            {
                theScale.x *= -1;
                GameObject.FindGameObjectWithTag("Player").transform.localScale = theScale;
            }
            
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