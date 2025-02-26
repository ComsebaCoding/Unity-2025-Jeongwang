#define UNITY_ADS // 유니티 광고 관련 함수를 사용하려면 필요한 디파인
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 게임 재시작을 위해서
using UnityEngine.UI;   // Text 컴포넌트를 써서 점수를 출력하기 위해

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
    public enum EnemyType
    {
        NoneType,
        Meteor,
        LargeMeteor,
        EnemyPlane,

        Count
    }

    public List<EnemyType> EnemyRandomTable;     // 에너미 소환 시 어떤 에너미가 스폰될 지 랜덤 선택할 테이블
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


    // Object Pooling - 오브젝트를 담아두는 연못을 만들어서 Destroy와 Instantiate를 최소화하는 기법
    public List<Meteor> MeteorPool;  // Meteor Pool 
    public List<LargeMeteor> LargeMeteorPool; // LargeMeteor Pool
    
    public void MeteorPooling(Meteor self)
    {        
        self.transform.position = new Vector3(0.0f, 0.0f, -100.0f);
        self.enabled = false;
        MeteorPool.Add(self);
        Debug.Log(self + "오브젝트 풀링 메테오");
    }
    public void LargeMeteorPooling(LargeMeteor self)
    {      
        self.transform.position = new Vector3(0.0f, 0.0f, -100.0f);
        self.enabled = false;
        LargeMeteorPool.Add(self);
        Debug.Log(self + "오브젝트 풀링 라지메테오");
    }

    public Meteor MeteorInstantiate(Vector3 StartPos)
    {
        Meteor m;
        if (MeteorPool.Count > 0)
        {
            m = MeteorPool[0];
            m.transform.position = StartPos;
            m.enabled = true;
            MeteorPool.Remove(m);
            if (m)
                Debug.Log("메테오를 오브젝트 풀에서 가져옴");
        }
        else
            m = Instantiate(MeteorPrefab, StartPos, Quaternion.identity);
        return m;
    }


    private int score = 0;
    private Text ScoreLabel;
    int highScore;
    private Text HighScoreLabel;

    private GameObject GameOverUI;

    private AudioSource audioSource;
    public AudioClip clickSound;    // 버튼 클릭 시
    public AudioClip countSound;    // 스코어 획득 시
    public AudioClip destroySound;  // 사망 폭발 시
    public AudioClip hitSound;      // 피격 시
    public AudioClip loseSound;     // 게임오버 시 
    public AudioClip shotSound;     // 레이저 사격

    public Meteor GetMeteorPrefab()
    {
        return MeteorPrefab;
    }

    public void PlayShotSound()
    {
        audioSource.PlayOneShot(shotSound);
    }
    public void PlayCountSound()
    {
        audioSource.PlayOneShot(countSound);
    }
    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
    public void PlayDestroySound()
    {
        audioSource.PlayOneShot(destroySound);
    }

    public void AddScore(int addValue)
    {
        score += addValue;
        ScoreLabel.text = "SCORE: " + score.ToString("D7");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.Log("GameManager.cs/Start() : Player is Not Found");

        ScoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();      
        if (ScoreLabel == null) Debug.Log("GameManager.cs/Start() : ScoreLabel is Not Found");
        else ScoreLabel.text = "SCORE: " + score.ToString("D7");
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreLabel = GameObject.Find("HighScoreLabel").GetComponent<Text>();
        if (HighScoreLabel == null) Debug.Log("GameManager.cs/Start() : HighScoreLabel is Not Found");
        else HighScoreLabel.text = "HIGHSCORE: " + highScore.ToString("D7");

        // Resources 폴더 내의 에셋 들은 리소스 로드 함수를 통해 프리팹이나 스프라이트 등으로 가져올 수 있다.
        // 모든 조상 클래스 변수는 자식 클래스 변수를 대입할 수 있다.
        MeteorPrefab = Resources.Load<GameObject>("Prefabs/Meteor").GetComponent<Meteor>();
        if (MeteorPrefab == null) Debug.Log("GameManager.cs/Start() : MeteorPrefab is Not Found");

        LargeMeteorPrefab = Resources.Load<GameObject>("Prefabs/LargeMeteor").GetComponent<LargeMeteor>();
        if (LargeMeteorPrefab == null) Debug.Log("GameManager.cs/Start() : LargeMeteorPrefab is Not Found");
               
        EnemyRandomTable.Add(EnemyType.Meteor);
        EnemyRandomTable.Add(EnemyType.Meteor);
        EnemyRandomTable.Add(EnemyType.Meteor);
        EnemyRandomTable.Add(EnemyType.LargeMeteor);
        EnemyRandomTable.Add(EnemyType.LargeMeteor);
        // enemyPrefab = LargeMeteorPrefab;

        GameOverUI = GameObject.Find("Canvas").transform.Find("GameOverUI").gameObject;
        if (GameOverUI == null) Debug.Log("GameManager.cs/Start() : GameOverUI is Not Found");
        GameOverUI.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.Log("GameManager.cs/Start() : audioSource is Not Found");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) ExitGame();

        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnCoolTime)
        {
            enemySpawnTimer -= enemySpawnCoolTime;
            Vector2 StartPos = Random.insideUnitCircle.normalized * 12.0f;
            switch (EnemyRandomTable[Random.Range(0, EnemyRandomTable.Count)])
            {
                case EnemyType.Meteor:
                    MeteorInstantiate(StartPos);
                    break;
                case EnemyType.LargeMeteor:
                    if (LargeMeteorPool.Count > 0)
                    {
                        LargeMeteor m = LargeMeteorPool[0];
                        m.transform.position = StartPos;
                        m.enabled = true;
                        LargeMeteorPool.Remove(m);
                        if (m)
                            Debug.Log("라지 메테오를 오브젝트 풀에서 가져옴");
                    }
                    else
                        Instantiate(LargeMeteorPrefab, StartPos, Quaternion.identity);
                    break;
                case EnemyType.EnemyPlane:
                    Debug.Log("에너미플레인 적을 아직 안 만들었습니다.");
                    break;
                default:
                    Debug.Log("있지도 않은 타입을 뽑으려는 정황이 포착되었습니다. 뭘 넣은겁니까?");
                    break;
            }
        }
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(loseSound);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            HighScoreLabel.text = "HIGHSCORE: " + highScore.ToString("D7");
        }
        Time.timeScale = 0.0f;
        GameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene(0);
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
