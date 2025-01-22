using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float ScrollSpeed = 3.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-ScrollSpeed*Time.deltaTime,0.0f, 0.0f));
        if (transform.position.x < -15.0f)
        {
            transform.Translate(new Vector3(30.0f, 0.0f, 0.0f));
        }
    }
}
