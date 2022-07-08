using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float timeFromLastSmash = 0f;
    public float smashWaitTime;
    private float waitTime;
    private float offsetTime;
    private bool ready = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips){
            waitTime = clip.length;
        }
        offsetTime = Random.Range(0.0f, (float) smashWaitTime)*1f;
        StartCoroutine(SmashOffset(offsetTime));
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
    IEnumerator SmashOffset(float time){ // This whole offset thing is to make sure that all the hammers don't smash at the exact same time
        ready = false;
        yield return new WaitForSeconds(time);
        ready = true;
        timeFromLastSmash = smashWaitTime + Time.deltaTime;
    }
    IEnumerator Smash(){
        animator.SetBool("isSmashing", true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isSmashing", false);
    }
}
