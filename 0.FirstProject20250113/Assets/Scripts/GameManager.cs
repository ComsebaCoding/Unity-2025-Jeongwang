using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject enemyPrefab;
    private Text scoreLabel;
    private GameObject GameOverTextLabel;

    float spawnTimer;
    public float respawnCooltime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        spawnTimer = 0.0f;
        // Resources ���� ������ Prefab ������ Enemy �������� �ε��ؿ´�
        enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");
        if (enemyPrefab == null)
        {
            Debug.Log("Enemy Prefab�� ã�� �� �������ϴ�.");
        }
        // ScoreText��� �̸��� GameObject�� ã�� Text ������Ʈ�� �����´�
        scoreLabel = GameObject.Find("ScoreText").GetComponent<Text>();      
        if (scoreLabel == null)
        {
            Debug.Log("ScoreText ������Ʈ�� Text ������Ʈ�� ã�� �� �������ϴ�.");
        }
        /*
        ������Ʈ�� Ʈ�������� �˻��ϴ� �Լ����� �������ϸ�
        Awake�� Start���� ���� ó���ϰ�, Update�� �浹 ���������� ���� ����.
          
        GameObject.Find("�̸�") �Լ��� 
        Scene�� '���� �ִ�(SetActive True)' "�̸�" ������Ʈ�� ã�ƿ´�.
        üũ�� �����ִ� ������Ʈ�� ã�ƿ� �� ����.

        GameObject.FindWithTag("�±�") 
        �� �±��� ������Ʈ �� �ϳ��� ã�ƿ´�.
        ���������� �����ִ� ������Ʈ�� ã�� �� ����.

        GameObject.FindObjectsWithTag("�±�") 
        �� �±��� ������Ʈ�� ���� ã�ƿ���, [] ���·� return�Ѵ�.
        ���� ������ ã�� �� ����.

        Transform.Find("�̸�") �Լ���
        �� ���ӿ�����Ʈ�� �ڽ� ������Ʈ �� "�̸�" ������Ʈ�� Ʈ�������� ã�ƿ´�.
        ���� �־ ã�ƿ� �� �ִ�.
        */
        GameOverTextLabel = 
            GameObject.Find("GameOverObject")   // ���� �ִ� ���ӿ�����Ʈ�� "�̸�"���� ã�ƿ´�.
            .transform.Find("GameOverText")     // �� ���ӿ�����Ʈ�� �ڽ� ������Ʈ�� Ʈ�������� "�̸�"���� ã�ƿ´�.
            .gameObject;                        // ã�Ƴ� ������Ʈ�� Ʈ�������� ���� ���ӿ�����Ʈ�� �����´�.
        if (GameOverTextLabel == null)
        {
            Debug.Log("GameOverText ������Ʈ�� ã�� �� �������ϴ�.");
        }
        else
        {
            GameOverTextLabel.SetActive(false);
        }
    }
    static public int score = 0;
    // Update is called once per frame
    void Update()
    {
        // �� �����Ӹ��� ���ھ �����ϴµ�, ���ŵɶ��� UI�� ������ �� ������?
        scoreLabel.text = "Score : " + score.ToString("D4"); // score ���� 10���� 4�ڸ��� ����
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer > respawnCooltime)
        {
            spawnTimer -= respawnCooltime;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-4.5f, 4.5f), 1.0f, Random.Range(-4.5f, 4.5f)), Quaternion.identity);
        }
    }

    public void GameOver()
    {
        GameOverTextLabel.SetActive(true);
        Time.timeScale = 0.0f;
        // Thread.Sleep(2500);
        // �κ�ũ �Լ��� ���� ���ϴ� �ð� ���Ŀ� Ư�� �Լ��� ȣ���Ѵ�.
        // �׷���, �� �ð��� ������ �� ���� Update�� ȣ��ȴ�.
        // ����, timeScale�� 0�̸� ������Ʈ�� �ȳ����ϱ� ȣ���� �� �ȴ�.
        // Invoke("Restart", 2.5f);
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        Debug.Log("����ŸƮ �ڷ�ƾ ����");
        yield return new WaitForSecondsRealtime(2.5f);
        // ���� �ð� ���� 2.5�ʰ� ������
        // RealTime �Լ��� Update�� ������ �����ϱ� ������ Timescale 0����� ��ȸ����
        Debug.Log("����ŸƮ �ڷ�ƾ 2.5�� ��� �Ϸ�");
        SceneManager.LoadScene(0);
    }
}