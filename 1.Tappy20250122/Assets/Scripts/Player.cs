using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myrigid;
    private Animator myAnimator;
    public float power = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        myrigid = gameObject.GetComponent<Rigidbody2D>();
        if (myrigid == null)
        {
            // Debug.Log("Player.cs : Rigidbody2D is Not Found.");
            Debug.Log("Player.cs에서 Player가 보유한 Rigidbody2D를 찾을 수 없었습니다.");
        }
        myAnimator = gameObject.GetComponent<Animator>();
        if(myAnimator == null)
        {
            Debug.Log("Player.cs에서 Player가 보유한 Animator를 찾을 수 없었습니다.");
        }
    }

    public void Up()
    {
        if (myrigid) myrigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        else Debug.Log("Jump를 시도했지만 Player에 Rigidbody2D가 없습니다.");
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
