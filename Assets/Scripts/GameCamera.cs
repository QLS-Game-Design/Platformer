using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Vector3 destPos;
    public Vector3 cam1;
    public Vector3 cam2;
    public Vector3 cam3;
    public Vector3 transitionCam;
    public float speed;
    int step = 0;
    Vector3[] positionArray;
    // Start is called before the first frame update
    void Start()
    {
        destPos = cam1;
        positionArray = new [] {cam1,cam2,cam3};
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,GameObject.FindGameObjectWithTag("Player").transform.position.y, -10);
    }

    IEnumerator Transition(){
        transform.position = transitionCam;
        yield return new WaitForSeconds(0.5f);
        transform.position = destPos;
    }
    public void NextPos()
    {
        step++;
        destPos = positionArray[step];
        StartCoroutine(Transition());
    }
}