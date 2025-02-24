#define UNITY_ADS // ����Ƽ ���� ���� �Լ��� ����Ϸ��� �ʿ��� ������
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

    // private Enemy enemyPrefab;      // ���� ��ȯ�� ��ǥ ���ʹ� ������

    public List<Enemy> EnemyRandomTable;     // ���ʹ� ��ȯ �� � ���ʹ̰� ������ �� ���� ������ ���̺�
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

    public Meteor GetMeteorPrefab()
    {
        return MeteorPrefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.Log("GameManager.cs/Start() : Player is Not Found");

        // Resources ���� ���� ���� ���� ���ҽ� �ε� �Լ��� ���� �������̳� ��������Ʈ ������ ������ �� �ִ�.
        // ��� ���� Ŭ���� ������ �ڽ� Ŭ���� ������ ������ �� �ִ�.
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
