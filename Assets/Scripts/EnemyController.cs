using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    private LayerMask mapLayer;
    public Transform detector;
    public float groundDetectionDistance = 0.3f;
    public float wallDetectionDistance = 0.05f;

    public float speed = 1.5f;
    public bool flipped = false; // false is right, true is left (to change default direction can just change this field in inspector)


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapLayer = LayerMask.GetMask("Map");
    //     Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
    }

    void FixedUpdate()
    {

        bool groundDetected = Physics2D.Raycast(detector.position, Vector2.down, groundDetectionDistance, mapLayer);
        bool wallDetected = Physics2D.Raycast(detector.position, transform.right, wallDetectionDistance, mapLayer);
        
        if (!groundDetected || wallDetected)
        {
            flipped = !flipped;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        rb.velocity = new Vector2(flipped ? -speed : speed, rb.velocity.y);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(detector.position, new Vector2(detector.position.x, detector.position.y - groundDetectionDistance));
    //    Gizmos.DrawLine(detector.position, new Vector2(detector.position.x + (flipped ? -wallDetectionDistance : wallDetectionDistance), detector.position.y));
    //}
}
