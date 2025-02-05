using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D myrigid;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    public AudioClip upSound;
    public AudioClip starSound;
    public AudioClip hitSound;
    public float power = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm == null) Debug.Log("GameManager is Not Found.");
        myrigid = gameObject.GetComponent<Rigidbody2D>();
        if (myrigid == null)
        {
            // Debug.Log("Player.cs : Rigidbody2D is Not Found.");
            Debug.Log("Player.cs에서 Player가 보유한 Rigidbody2D를 찾을 수 없었습니다.");
        }
        myAnimator = gameObject.GetComponent<Animator>();
        if(myAnimator == null)
            Debug.Log("Player.cs에서 Player가 보유한 Animator를 찾을 수 없었습니다.");
        myAudioSource = gameObject.GetComponent<AudioSource>();
        if (myAudioSource == null) Debug.Log("AudioSource is Not Found.");
    }

    public void Up()
    {
        if (myrigid) myrigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        else Debug.Log("Jump를 시도했지만 Player에 Rigidbody2D가 없습니다.");
        myAudioSource.PlayOneShot(upSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.UpArrow)
        //    || Input.GetKeyDown(KeyCode.Mouse0)
        //    || Input.GetKeyDown(KeyCode.Mouse1)
            )
        {
            Up();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Hit!"); // 제거
        if(myAnimator) 
            myAnimator.SetBool("isDead", true);
        Invoke("OnDead", 1.0f);
        myAudioSource.PlayOneShot(hitSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ScrollingRandom scr = other.GetComponent<ScrollingRandom>();
        if (scr)
        {
            scr.Hide();
            GameManager.score += 100;
            gm.UpdateScore();
            myAudioSource.PlayOneShot(starSound);
        }
    }

    public GameManager gameManager;
    void OnDead()
    {
        if (gameManager)
            gameManager.GameOver();
        else
            Debug.Log("public GameManager를 등록하지 않았습니다.");
    }
}
