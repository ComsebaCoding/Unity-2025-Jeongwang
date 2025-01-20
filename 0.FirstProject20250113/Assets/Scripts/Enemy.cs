using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager myGameManager;
    Vector3 direction;          // ���� �̵� ����
    public float speed = 3.0f;  // ���� �̵� �ӵ�
    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (myGameManager == null)
        {
            Debug.Log("Enemy ������Ʈ���� ���� �Ŵ����� ã�µ� ���� �Ͽ����ϴ�.");
        }
        direction = Random.insideUnitSphere;    // ���� ���� �̱�
        direction.y = 0.0f;     // y�� ��ǥ�� �翵
        direction.Normalize();  // direction ������ ũ�⸦ 1�� ����ȭ
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