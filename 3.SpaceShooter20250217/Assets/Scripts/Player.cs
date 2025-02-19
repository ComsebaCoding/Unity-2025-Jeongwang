using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 direction;
    public float speed = 3.0f;
    int hp = 5;

    private GameObject NormalLaserPrepab;       // 평타 투사체 프리팹
    float normalShootTimer = 0.0f;              // 투사체 발사 타이머
    public float normalLaserCoolTime = 0.2f;    // 투사체 발사 쿨타임

    // Start is called before the first frame update
    void Start()
    {
        NormalLaserPrepab = Resources.Load<GameObject>("Prefabs/NormalLaser_Player");
        if (NormalLaserPrepab == null) Debug.Log("Player.cs : Prefabs/NormalLaser_Player is Not Found.");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : 키 입력에 따라 방향 설정
        direction = Vector2.zero;
        direction.x += Input.GetKey(KeyCode.LeftArrow) ? -1.0f : 0.0f;
        direction.x += Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0.0f;
        direction.y += Input.GetKey(KeyCode.DownArrow) ? -1.0f : 0.0f;
        direction.y += Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0.0f;
        direction.Normalize(); // 계산한 방향 벡터의 크기를 1로 조절 (대각선처리)


        // Vector2.ClampMagnitude(벡터, 길이) // 벡터의 크기와 인자로 들어온 길이중 더 큰 값을 return 
        // Vector2.Distance(벡터1, 벡터2) // 두 벡터 사이의 거리를 return
        // Vector2.Equals // 두 게임오브젝트가 가진 '벡터'가 동일하면 True, 다르면 false
        // Vector2.Lerp(벡터1, 벡터2, 보간값) // 보간값을 기준으로 벡터1, 벡터2를 보간
        // Vector2.Reflect(방향벡터, 법선벡터) // 법선벡터 : 내가 반사시키려는 표면에 수직 방향인 벡터
        // 방향 벡터를 법선 벡터 기준으로 반사 시켰을때 나오는 반사된 벡터를 반환
        
        // 입력이 존재할 경우에만 이동
        if (direction.sqrMagnitude > 0.1f)
        {
            // 현재 각도 = z축기준 회전된 오일러각도
            float currentAngle = transform.rotation.eulerAngles.z;
            // 내가 가고자 하는 각도 = Up벡터와 direction 사이의 각도
            // Vector2.SignedAngle(벡터1, 벡터2) 함수는 두 벡터 사이의 각도를 return 
            float targetAngle = Vector2.SignedAngle(Vector2.up, direction);
            
            // 현재 속도를 저장
            float curVelocity = 0.0f;
            // 현재 각도를 부드럽게 회전한 각도를 구하는 함수
            currentAngle = Mathf.SmoothDampAngle(
                currentAngle    // 현재 각도
                , targetAngle   // 내가 가고자 하는 타겟 각도
                , ref curVelocity   // 현재 속도를 계산해서 돌려줌
                , Time.deltaTime    // 스무스하게 회전하는데 걸리는 시간, 프레임비례
                , 500); // 부드럽게 회전하는 최고 속도

            // Z축 회전 함수
            // Quaternion.Euler() : 인자로 들어온 각도를 오일러 각도로 변환하는 함수
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentAngle);
        }
        // 플레이어 위쪽 방향으로 이동
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
        
        normalShootTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && normalShootTimer >= normalLaserCoolTime)            
        {
            normalShootTimer = 0.0f;
            
            // 플레이어 위치에 바로 투사체 생성
            Instantiate(NormalLaserPrepab, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        --hp;
        // TODO : 체력이 0이 되면 게임오버 처리
    }
}