using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBlock : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer sr;
    public float scrollSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D == null) Debug.Log("ScrollingBlock.cs : Rigidbody2D is not found");
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) Debug.Log("ScrollingBlock.cs : SpriteRenderer is not found");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-scrollSpeed * Time.deltaTime, 0.0f, 0.0f));
        if (transform.position.x < -15.0f)
        {
            transform.Translate(new Vector3(30.0f, 0.0f, 0.0f));
            boxCollider2D.enabled = sr.enabled = (Random.Range(0, 2) == 0);
        }
    }
}