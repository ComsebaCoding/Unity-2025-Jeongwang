using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLaser : MonoBehaviour
{
    public float LaserSpeed = 5.0f;
    public int AttackDamage = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * LaserSpeed * Time.deltaTime, Space.Self);
        // 매그니튜드 : 오브젝트의 화면 중심으로부터 떨어진 거리
        if (transform.position.magnitude > 15.0f)
        {
            // 화면 중심으로부터 15보다 멀어지면 본인 오브젝트를 제거.
            Destroy(gameObject);
            // 오브젝트 풀링 구현할 때 디스트로이를 변경할 예정
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); // 나중에 오브젝트 풀링으로 변경
    }
}
