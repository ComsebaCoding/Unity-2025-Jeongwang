using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy // 모노비헤이비어는 에너미가 이미 상속받았음
{
    private Rigidbody2D myrigid;
    public Vector3 direction = Vector3.zero;    // 메테오가 움직일 방향
    public float speed = 3.0f;                  // 메테오의 속력

    private GameObject dustPrefab;
    // Start is called before the first frame update
    void Start()
    {
        myrigid = GetComponent<Rigidbody2D>();
        if (myrigid == null) Debug.Log("Meteor.cs/Start() : Rigidbody2D is Not Found");
        else myrigid.angularVelocity = Random.Range(120.0f, 360.0f);

        // direction 벡터가 영벡터일 경우
        // 메테오 기준으로 플레이어의 방향을 direction 변수에 저장
        if (direction == Vector3.zero)
        {
            // 벡터 빼기 연산
            // 내가 가려는 방향 = 내가 가고 싶은 위치 - 현재 나의 위치
            // 벡터연산 A->B 방향벡터 : (B-A).노멀라이즈 
            Vector3 delta = GameManager.instance.GetPlayerPos() - transform.position;
            direction = delta.normalized;
        }

        dustPrefab = Resources.Load<GameObject>("Prefabs/Dust");
        if (dustPrefab == null) Debug.Log("Meteor.cs/Start() : DustPrefab is Not Found");
    }
    
    // override 키워드는 내가 부모의 함수를 상속받아서 재정의 했다는 뜻
    // Update is called once per frame
    override protected void Update()
    {
        // Enemy 의 Update를 먼저 실행
        base.Update(); // 부모 클래스의 Update 함수

        // direction 방향으로 이동
        transform.position += direction * speed * Time.deltaTime;
    }
 
    protected override void OnDead()
    {
        // TODO : Dust 생성
        // 오브젝트 풀링 구현 시 이미 있는 데이터인지 검사 후, 없을 때만 생성, 있으면 그냥 갖다씀
        for (int i = 0; i < 3; ++i)
            Instantiate(dustPrefab, transform.position, Quaternion.identity);
    }


}
