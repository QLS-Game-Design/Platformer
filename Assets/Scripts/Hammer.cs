using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float timeFromLastSmash = 0f;
    public float smashWaitTime = 0.5f;
    public float waitTime;
    public float offsetTime;
    private bool ready = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(SmashOffset());
    }

    void Update()
    {
        if (timeFromLastSmash > smashWaitTime && ready){
            StartCoroutine(Smash());
            timeFromLastSmash = 0f;
        } else {
            timeFromLastSmash += Time.deltaTime;
        }
    }
    IEnumerator SmashOffset(){ // This whole offset thing is to make sure that all the hammers don't smash at the exact same time
        yield return new WaitForSeconds(offsetTime);
        ready = true;
    }
    IEnumerator Smash(){
        animator.SetBool("isSmashing", true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isSmashing", false);
    }
}
