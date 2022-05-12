using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    public float maxHealth = 20f;
    public float health;
    public Vector3 target;
    public int stage;
    private float stageOneThreshold = 15f;
    private float stageTwoThreshold = 10f;
    public GameObject player;
    public GameObject bulletPrefab;
    public float flySpeedX = 0.05f;
    private float centerX = 0;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=stageTwoThreshold){
            ShootDown();
        } 
        if (health<=stageOneThreshold){
            flyUp();
        } else{
            target.x = transform.position.x;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
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

    private void flyUp(){
        transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
        target.x += flySpeedX;
    }
    private void ShootDown(){
        player = GameObject.FindGameObjectWithTag("Player");
        float theta = Mathf.Atan((player.transform.position.x-transform.position.x)/(transform.position.y-player.transform.position.y))*(180/Mathf.PI);
        currentEulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, theta);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
        GameObject clone = Instantiate(bulletPrefab,transform.position, transform.rotation);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        } else if (collision.collider.gameObject.tag == "Map"){
            Debug.Log("hit wall");
            flySpeedX *= -1;
        }
        Debug.Log(collision.collider.gameObject.tag);
    }
}
