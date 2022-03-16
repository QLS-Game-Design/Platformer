using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elon : MonoBehaviour
{
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(Vector3.back * Time.deltaTime);
        }
    }
}
