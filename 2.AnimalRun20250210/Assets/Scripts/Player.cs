using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myrigid;
    private Animator myAnimator;
    public float power;
    // Start is called before the first frame update
    void Start()
    {
        myrigid = GetComponent<Rigidbody2D>();
        if (myrigid == null) Debug.Log("Player.cs : Rigidbody2D is not found");
        myAnimator = GetComponent<Animator>();
        if (myAnimator == null) Debug.Log("Player.cs : Animator is not found");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.Log("Player.cs : GameManager is not found");
    }

    public void Jump()
    {
        if (myAnimator.GetBool("isJump"))
            return;

        myrigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        myAnimator.SetBool("isJump", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimator.SetBool("isJump", false);
    }

    private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.GameOver();
    }
}
