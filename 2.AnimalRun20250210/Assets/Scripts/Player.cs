using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myrigid;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    public float power;

    public AudioClip jumpSound;     // 점프시작
    public AudioClip groundSound;   // land (착지)
    public AudioClip hitSound;	    // 박스와 충돌
    // Start is called before the first frame update
    void Start()
    {
        myrigid = GetComponent<Rigidbody2D>();
        if (myrigid == null) Debug.Log("Player.cs : Rigidbody2D is not found");
        myAnimator = GetComponent<Animator>();
        if (myAnimator == null) Debug.Log("Player.cs : Animator is not found");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.Log("Player.cs : GameManager is not found");
        myAudioSource = GetComponent<AudioSource>();
        if (myAudioSource == null) Debug.Log("Player.cs : AudioSource is not found");
    }

    public void Jump()
    {
        if (myAnimator.GetBool("isJump"))
            return;

        myrigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        myAnimator.SetBool("isJump", true);
        myAudioSource.PlayOneShot(jumpSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimator.SetBool("isJump", false);
        myAudioSource.PlayOneShot(groundSound);
    }

    private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        myAudioSource.PlayOneShot(hitSound);
        gameManager.GameOver();
    }
}
