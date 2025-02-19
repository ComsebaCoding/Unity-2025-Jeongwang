using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스, 직접 컴포넌트로 사용할 수 없음
// 반드시 컴포넌트의 부모로 상속받아서 사용할 것
abstract public class Enemy : MonoBehaviour
{
    public int hp;
    // virtual 키워드는 가상 함수를 만드는 키워드이다.
    // 가상 함수는 내가 자식에게 상속 시켰을 때 자식이 새로 정의해서 기능을 변경할 수 있다.
    // Update is called once per frame
    virtual protected void Update()
    {
        // 매그니튜드 : 오브젝트의 화면 중심으로부터 떨어진 거리
        if (transform.position.magnitude > 15.0f)
        {
            // 화면 중심으로부터 15보다 멀어지면 본인 오브젝트를 제거.
            Destroy(gameObject);
            // 오브젝트 풀링 구현할 때 디스트로이를 변경할 예정
        }
    }

    // 데미지 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        --hp;
        if (hp <= 0)
        {
            OnDead(); // 추상 함수이므로 자식이 구현해줘서 자식 별로 다른 Dead Action 발동!
            Destroy(gameObject);
        }
    }

    // 추상 함수, 자식 클래스가 구현해줘야함
    abstract protected void OnDead(); 
}
