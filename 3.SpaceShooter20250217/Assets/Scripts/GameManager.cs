#define UNITY_ADS // 유니티 광고 관련 함수를 사용하려면 필요한 디파인
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private Player player;
    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    // private Enemy enemyPrefab;      // 현재 소환할 대표 에너미 프리팹

    public List<Enemy> EnemyRandomTable;     // 에너미 소환 시 어떤 에너미가 스폰될 지 랜덤 선택할 테이블
    // 수정 예정, 전체 분의 비중으로 뽑는 구조이므로 자세한 세부 확률(47%, 19%...)을 정의 하는데에는 부적합
    // 다만 내 게임이 전체 분의 비중으로 뽑는 구조인게 맞다면 유지해도 좋다

    // 순열 조합
    // n 개의 패턴을 차례대로 배치하는 경우의 수 = n!
    // 순열 : n P r
    // 조합 : n 개의 패턴 중 r 개를 뽑아서 r 개를 차례대로 배치하는 경우의 수 = n C r = n!/((n-r)!r!)

    // n C r = n! / (n-r)!r!
    // n C 5 = n! / (n-5)! * 5!
    private Meteor MeteorPrefab;    // Meteor 프리팹을 로드
    private LargeMeteor LargeMeteorPrefab; // Large Meteor 프리팹을 로드
    float enemySpawnTimer;
    public float enemySpawnCoolTime = 3.0f;

    public Meteor GetMeteorPrefab()
    {
        return MeteorPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.Log("GameManager.cs/Start() : Player is Not Found");

        // Resources 폴더 내의 에셋 들은 리소스 로드 함수를 통해 프리팹이나 스프라이트 등으로 가져올 수 있다.
        // 모든 조상 클래스 변수는 자식 클래스 변수를 대입할 수 있다.
        MeteorPrefab = Resources.Load<GameObject>("Prefabs/Meteor").GetComponent<Meteor>();
        if (MeteorPrefab == null) Debug.Log("GameManager.cs/Start() : MeteorPrefab is Not Found");

        LargeMeteorPrefab = Resources.Load<GameObject>("Prefabs/LargeMeteor").GetComponent<LargeMeteor>();
        if (LargeMeteorPrefab == null) Debug.Log("GameManager.cs/Start() : LargeMeteorPrefab is Not Found");
               
        EnemyRandomTable.Add(MeteorPrefab);
        EnemyRandomTable.Add(MeteorPrefab);
        EnemyRandomTable.Add(MeteorPrefab);
        EnemyRandomTable.Add(LargeMeteorPrefab);
        EnemyRandomTable.Add(LargeMeteorPrefab);
        // enemyPrefab = LargeMeteorPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) ExitGame();

        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnCoolTime)
        {
            enemySpawnTimer -= enemySpawnCoolTime;
                    
            Enemy enemyPrefab = EnemyRandomTable[Random.Range(0, EnemyRandomTable.Count)];
            Vector2 StartPos = Random.insideUnitCircle.normalized * 12.0f;
            if (enemyPrefab)
                Instantiate(enemyPrefab, StartPos, Quaternion.identity);
        }
    }

    private void ExitGame()
    {
        // UNITY_EDITOR_WIN 유니티 에디터, 그런데 윈도우즈만
        // UNITY_EDITOR_OSX 유니티 에디터, 그런데 맥북만

        // 에디터가 아닌 경우, 특정 플랫폼일 때의 define
        // UNITY_STANDALONE         모든 컴퓨터
        // UNITY_STANDALONE_WIN     윈도우즈 컴퓨터
        // UNITY_STANDALONE_OSX     맥북
        // UNITY_STANDALONE_LINUX   리눅스
        // UNITY_IOS                아이폰
        // UNITY_ANDROID            안드로이드 폰
        // UNITY_PS4                플레이스테이션4
        // UNITY_XBOXONE            지존박스

        // 에디터인지 빌드한 프로그램인지 무관함
        // UNITY_64     64비트 플랫폼

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 게임 플레이 정지
#elif UNITY_STANDALONE_WIN
        Application.Quit(); // 어플리케이션 프로그램 종료
#endif
    }
}
