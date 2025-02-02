using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager myGameManager;
    Vector3 direction;          // 적의 이동 방향
    public float speed = 3.0f;  // 적의 이동 속도
    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (myGameManager == null)
        {
            Debug.Log("Enemy 컴포넌트에서 게임 매니저를 찾는데 실패 하였습니다.");
        }
        direction = Random.insideUnitSphere;    // 랜덤 방향 뽑기
        direction.y = 0.0f;     // y축 좌표를 사영
        direction.Normalize();  // direction 벡터의 크기를 1로 정규화
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * direction * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            myGameManager.GameOver();
        }


        if (collision.gameObject.CompareTag("WallH"))
            direction.z = -direction.z;
        if (collision.gameObject.CompareTag("WallV"))
            direction.x = -direction.x;
    }
}