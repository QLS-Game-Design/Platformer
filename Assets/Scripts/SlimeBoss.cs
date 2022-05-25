using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    private LayerMask playerLayer;
    public float maxHealth = 20f;
    public float health;
    public Vector3 target;
    public int stage;
    public float stageOneThreshold = 199f;
    public float stageTwoThreshold = 120f;
    public float stageThreeThreshold = 80f;
    public float stageFourThreshold = 40f;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject healthBar;
    public float flySpeedX = 0.05f;
    private float centerX = 0;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    private float timeFromLastShot = 0f;
    public float shotWaitTime = 0.5f;
    private float timeFromLastSmash = 0f;
    public float smashWaitTime = 3f;
    private bool runningSmashAttack;
    private bool tracking;
    private bool attacking;
    private float attackTime;
    public float smashAttackPause = 0.75f;
    public float smashAttackDuration = 1f;
    public float smashSpeed = 0.05f;
    private bool hitGround;
    public GameObject deathEffect;
    public GameObject levelLoader;
    public GameObject slimePrefab;
    public GameObject bigSlimePrefab;
    private AudioSource bossDeathAudio;
    private float timeFromLastSpawn = 0f;
    public float spawnWaitTime = 0.5f;
    public GameObject spawner;
    public float chaseSpeed = 0.2f;
    public float chaseSpeedStageTwo = 0.26f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        stage = 0;
        playerLayer = LayerMask.GetMask("Player");
        bossDeathAudio = GameObject.Find("BossDeathAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBar.GetComponent<HealthBar>().updateValue(health / maxHealth);
        if (runningSmashAttack)
        {
            if (tracking)
            {
                target.x = player.transform.position.x;
                transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed);
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < 0.1)
                {
                    //Debug.Log(transform.position.x - player.transform.position.x);
                    tracking = false;
                    StartCoroutine(SmashAttackPause());
                }
            } else if (attacking)
            {
                if (!hitGround)
                {
                    transform.position += Vector3.down * smashSpeed;
                }
                attackTime += Time.deltaTime;
            }
            if (attackTime > smashAttackDuration)
            {
                Debug.Log("stopping smash attack");
                timeFromLastSmash = 0f;
                tracking = false;
                attacking = false;
                attackTime = 0f;
                runningSmashAttack = false;
            }
            return;
        }

        if (health<=stageTwoThreshold){
            this.GetComponent<SpriteRenderer>().color = Color.red; // make boss look angry
            chaseSpeed = chaseSpeedStageTwo;
            ShootDown();
        } 
        if (health<=stageOneThreshold){
            flyUp();
            if (timeFromLastSmash > smashWaitTime)
            {
                runningSmashAttack = true;
                tracking = true;
                Debug.Log("starting smash attack & tracking");
            } else
            {
                timeFromLastSmash += Time.deltaTime;
            }
        } else {
            target.x = transform.position.x;
        }
        if (health<=stageThreeThreshold){
            if(health <= stageFourThreshold){
                SpawnSlimes(bigSlimePrefab);
            } else {
                SpawnSlimes(slimePrefab);
            }
        }
    }

    IEnumerator SmashAttackPause()
    {
        Debug.Log("stopping tracking");
        yield return new WaitForSeconds(smashAttackPause);
        attacking = true;
        Debug.Log("starting attacking");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            levelLoader.GetComponent<LevelLoader>().isGameOver = true;
            StartCoroutine(Die());
        }
    }
    public void SpawnSlimes(GameObject enemyPrefab){
        if (timeFromLastSpawn > spawnWaitTime){
            Instantiate(enemyPrefab, spawner.transform.position, spawner.transform.rotation);
            timeFromLastSpawn = 0;
        } else{
            timeFromLastSpawn += Time.deltaTime;
        }
        
    }
    // private void OnDrawGizmos()
    // {
    //    Gizmos.DrawLine(spawner.transform.position, new Vector2(spawner.transform.position.x, spawner.transform.position.y - 1f));
    // }
    IEnumerator Die()
    {
        bossDeathAudio.Play();
        Instantiate(deathEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        GameObject.Destroy(gameObject);
    }
    private void flyUp(){
        transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed);
        target.x += flySpeedX;
    }
    private void ShootDown(){
        player = GameObject.FindGameObjectWithTag("Player");
        float theta = Mathf.Atan((player.transform.position.x-transform.position.x)/(transform.position.y-player.transform.position.y))*(180/Mathf.PI);
        currentEulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, theta);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
        if (timeFromLastShot > shotWaitTime){
            GameObject clone = Instantiate(bulletPrefab, spawner.transform.position, transform.rotation);
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
        if (collision.gameObject.tag == "Map")
        {
            hitGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            hitGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            flySpeedX *= -1;
        }
    }
}
