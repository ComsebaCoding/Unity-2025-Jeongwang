#define UNITY_ADS // ����Ƽ ���� ���� �Լ��� ����Ϸ��� �ʿ��� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ���� ������� ���ؼ�
using UnityEngine.UI;   // Text ������Ʈ�� �Ἥ ������ ����ϱ� ����

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

    // private Enemy enemyPrefab;      // ���� ��ȯ�� ��ǥ ���ʹ� ������
    public enum EnemyType
    {
        NoneType,
        Meteor,
        LargeMeteor,
        EnemyPlane,

        Count
    }

    public List<EnemyType> EnemyRandomTable;     // ���ʹ� ��ȯ �� � ���ʹ̰� ������ �� ���� ������ ���̺�
    // ���� ����, ��ü ���� �������� �̴� �����̹Ƿ� �ڼ��� ���� Ȯ��(47%, 19%...)�� ���� �ϴµ����� ������
    // �ٸ� �� ������ ��ü ���� �������� �̴� �����ΰ� �´ٸ� �����ص� ����

    // ���� ����
    // n ���� ������ ���ʴ�� ��ġ�ϴ� ����� �� = n!
    // ���� : n P r
    // ���� : n ���� ���� �� r ���� �̾Ƽ� r ���� ���ʴ�� ��ġ�ϴ� ����� �� = n C r = n!/((n-r)!r!)

    // n C r = n! / (n-r)!r!
    // n C 5 = n! / (n-5)! * 5!
    private Meteor MeteorPrefab;    // Meteor �������� �ε�
    private LargeMeteor LargeMeteorPrefab; // Large Meteor �������� �ε�
    float enemySpawnTimer;
    public float enemySpawnCoolTime = 3.0f;


    // Object Pooling - ������Ʈ�� ��Ƶδ� ������ ���� Destroy�� Instantiate�� �ּ�ȭ�ϴ� ���
    public List<Meteor> MeteorPool;  // Meteor Pool 
    public List<LargeMeteor> LargeMeteorPool; // LargeMeteor Pool
    
    public void MeteorPooling(Meteor self)
    {        
        self.transform.position = new Vector3(0.0f, 0.0f, -100.0f);
        self.enabled = false;
        MeteorPool.Add(self);
        Debug.Log(self + "������Ʈ Ǯ�� ���׿�");
    }
    public void LargeMeteorPooling(LargeMeteor self)
    {      
        self.transform.position = new Vector3(0.0f, 0.0f, -100.0f);
        self.enabled = false;
        LargeMeteorPool.Add(self);
        Debug.Log(self + "������Ʈ Ǯ�� �������׿�");
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
                Debug.Log("���׿��� ������Ʈ Ǯ���� ������");
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
    public AudioClip clickSound;    // ��ư Ŭ�� ��
    public AudioClip countSound;    // ���ھ� ȹ�� ��
    public AudioClip destroySound;  // ��� ���� ��
    public AudioClip hitSound;      // �ǰ� ��
    public AudioClip loseSound;     // ���ӿ��� �� 
    public AudioClip shotSound;     // ������ ���

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

        // Resources ���� ���� ���� ���� ���ҽ� �ε� �Լ��� ���� �������̳� ��������Ʈ ������ ������ �� �ִ�.
        // ��� ���� Ŭ���� ������ �ڽ� Ŭ���� ������ ������ �� �ִ�.
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
                            Debug.Log("���� ���׿��� ������Ʈ Ǯ���� ������");
                    }
                    else
                        Instantiate(LargeMeteorPrefab, StartPos, Quaternion.identity);
                    break;
                case EnemyType.EnemyPlane:
                    Debug.Log("���ʹ��÷��� ���� ���� �� ��������ϴ�.");
                    break;
                default:
                    Debug.Log("������ ���� Ÿ���� �������� ��Ȳ�� �����Ǿ����ϴ�. �� �����̴ϱ�?");
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
        // UNITY_EDITOR_WIN ����Ƽ ������, �׷��� �������
        // UNITY_EDITOR_OSX ����Ƽ ������, �׷��� �ƺϸ�

        // �����Ͱ� �ƴ� ���, Ư�� �÷����� ���� define
        // UNITY_STANDALONE         ��� ��ǻ��
        // UNITY_STANDALONE_WIN     �������� ��ǻ��
        // UNITY_STANDALONE_OSX     �ƺ�
        // UNITY_STANDALONE_LINUX   ������
        // UNITY_IOS                ������
        // UNITY_ANDROID            �ȵ���̵� ��
        // UNITY_PS4                �÷��̽����̼�4
        // UNITY_XBOXONE            �����ڽ�

        // ���������� ������ ���α׷����� ������
        // UNITY_64     64��Ʈ �÷���

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ���� �÷��� ����
#elif UNITY_STANDALONE_WIN
        Application.Quit(); // ���ø����̼� ���α׷� ����
#endif
    }
}
