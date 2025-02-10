using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 5;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f));
        if(transform.position.x < -15.0f)
            transform.Translate(new Vector3(30.0f, 0.0f, 0.0f));
    }
}
