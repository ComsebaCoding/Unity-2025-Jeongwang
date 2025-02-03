using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingRandom : MonoBehaviour
{
    private Collider2D col2D;
    private SpriteRenderer mysr; 
    public float ScrollSpeed = 3.0f;
    void Start()
    {
        col2D = GetComponent<Collider2D>();
        if (col2D == null)
            Debug.Log("ScrollingRandom.cs : Collider2D is Not Found.");
        mysr = GetComponent<SpriteRenderer>();
        if (mysr)
            Debug.Log("ScrollingRandom.cs : SpriteRenderer is Not Found.");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-ScrollSpeed * Time.deltaTime, 0.0f, 0.0f));
        if (transform.position.x < -15.0f)
        {
            transform.Translate(new Vector3(30.0f, 0.0f, 0.0f));

            if (Random.Range(0, 2) == 0)
                col2D.enabled = mysr.enabled = true;
            else
                col2D.enabled = mysr.enabled = false;
        }
    }
}
